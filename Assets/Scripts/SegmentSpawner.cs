using UnityEngine;
using UnityEngine.Rendering.Universal;

public class SegmentSpawner : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject segmentPrefab, segmentPrefab2;
    [SerializeField] private float gapSize;
    [SerializeField] private float maxDistanceFromPlayer;


    private Renderer lastRender, currentRender;
    private GameObject lastGameObject, currentGameobject;

    private void Start()
    {
        // Segment 1
        lastGameObject = Instantiate(segmentPrefab, new Vector3(player.transform.position.x, player.transform.position.y - 1, 0), Quaternion.identity, transform);
        lastRender = lastGameObject.GetComponent<Renderer>();


        // Segment 2
        currentGameobject = Instantiate(segmentPrefab, transform);
        currentRender = currentGameobject.GetComponent<Renderer>();


        // Last Render and current render
        float xSpawnPosition = lastRender.bounds.max.x + (currentRender.bounds.size.x / 2) + gapSize;
        currentGameobject.transform.position = new Vector3(xSpawnPosition, player.transform.position.y - 1 , 0);

        lastGameObject = currentGameobject;
        lastRender = currentRender;
    }


    private void Update()
    {
        // 18.43 + 7.5 + 0.5 = 26.48
        // Last GameObject.bounds.max.x < player.position.x + maxDistanceFromPlayer

        if (lastRender.bounds.max.x > player.transform.position.x + maxDistanceFromPlayer)
        {
            currentGameobject = Instantiate(segmentPrefab2, transform);
            currentRender = currentGameobject.GetComponent<Renderer>();

            float xSpawnPosition = lastRender.bounds.max.x + (currentRender.bounds.size.x / 2) + gapSize;
            currentGameobject.transform.position = new Vector3(xSpawnPosition, player.transform.position.y - 1, 0);

            lastGameObject = currentGameobject;
            lastRender = currentRender;
        }
    }
}
