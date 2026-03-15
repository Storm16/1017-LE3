using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SegmentSpawner : MonoBehaviour
{
    [SerializeField] private GameObject segmentPrefab, segmentPrefab2;
    [SerializeField] private float maxDistanceFromPlayer = 30;
    [SerializeField] private int segmentListSize = 5;

    private Renderer lastRender, currentRender;
    private GameObject lastGameObject, currentGameObject;
    private List<GameObject> segments = new();
    private float gapSize = 0.5f;
    private GameObject player;

    public void Initialize()
    {
        player = GameManager.Instance.Player.gameObject;

        // Segment 1
        lastGameObject = Instantiate(segmentPrefab, new Vector3(player.transform.position.x, player.transform.position.y - 1, 0), Quaternion.identity, transform);
        lastRender = lastGameObject.GetComponentInChildren<Renderer>();
        segments.Add(lastGameObject);

        // Segment 2
        currentGameObject = Instantiate(segmentPrefab, transform);
        currentRender = currentGameObject.GetComponentInChildren<Renderer>();
        segments.Add(currentGameObject);

        float xSpawnPosition = lastRender.bounds.max.x + (currentRender.bounds.size.x / 2) + gapSize;
        currentGameObject.transform.position = new Vector3(xSpawnPosition, player.transform.position.y - 1, 0);

        lastGameObject = currentGameObject;
        lastRender = currentRender;
    }

    private void Update()
    {
        if (lastRender == null || player == null) return;

        if (lastRender.bounds.max.x < player.transform.position.x + maxDistanceFromPlayer)
        {
            gapSize = Random.Range(0.5f, 1.5f);
            float heightOffset = Random.Range(-1.5f, 1.5f);

            currentGameObject = Instantiate(segmentPrefab2, transform);
            currentRender = currentGameObject.GetComponentInChildren<Renderer>();

            float xSpawnPosition = lastRender.bounds.max.x + (currentRender.bounds.size.x / 2) + gapSize;
            currentGameObject.transform.position = new Vector3(xSpawnPosition, lastGameObject.transform.position.y + heightOffset, 0);
            segments.Add(currentGameObject);

            if (segments.Count > segmentListSize)
            {
                Destroy(segments[0]);
                segments.RemoveAt(0);
            }

            lastGameObject = currentGameObject;
            lastRender = currentRender;
        }
    }

    public void Reset()
    {
        lastRender = null;
        currentRender = null;

        lastGameObject = null;
        currentGameObject = null;

        foreach (GameObject gameObject in segments)
        {
            Destroy(gameObject);
        }

        segments.Clear();
    }
}