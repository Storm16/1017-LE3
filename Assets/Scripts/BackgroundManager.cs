using UnityEngine;
using System.Collections.Generic;

public class BackgroundManager : MonoBehaviour
{
    [SerializeField] private GameObject backgroundPrefab;
    [SerializeField] private Camera cam;
    [SerializeField] private float xBuffer = 3;

    private Transform lastBackground;
    private Renderer lastRenderer;
    private float backgroundWidth;
    private float nextSpawnAtCamRightX; // world X where camera-right must reach to spawn again

    private List<GameObject> backgrounds = new List<GameObject>();
    private int objectPoolSize = 3;

    private void Start()
    {
        if (!cam) cam = Camera.main;

        // Create object pool
        for (int i = 0; i < objectPoolSize; i++)
        {
            GameObject go = Instantiate(backgroundPrefab, transform);
            ReturnToPool(go);
            backgrounds.Add(go);
        }
    }

    public void Initialize()
    {
        lastBackground = GetNextObject().transform;
        lastRenderer = lastBackground.GetComponent<Renderer>();
        backgroundWidth = lastRenderer.bounds.size.x;
        lastRenderer.sortingOrder = 0;

        // Set first trigger based on the first background's right edge
        UpdateNextSpawnTrigger();
    }

    // Itterate through the backgrounds to get one that is inactive.
    private GameObject GetNextObject()
    {
        foreach (GameObject go in backgrounds)
        {
            if (!go.activeSelf)
            {
                go.SetActive(true);
                return go;
            }
        }

        return null;
    }

    private void ReturnToPool(GameObject background)
    {
        background.SetActive(false);
    }

    private void Update()
    {
        if (lastBackground == null) return;
        float haldCamWidth = cam.orthographicSize * cam.aspect;
        float camRightEdge = cam.transform.position.x + haldCamWidth;

        if (camRightEdge >= nextSpawnAtCamRightX)
        {
            SpawnNextToRight();
            UpdateNextSpawnTrigger();
        }
    }

    private void SpawnNextToRight()
    {
        Vector3 spawnPos = lastBackground.position;
        spawnPos.x += backgroundWidth;

        // Check if there are no inactive objects
        if (GetNextObject() == null)
        {
            float previousDistance = 0;
            float distance;
            GameObject objectToReturn = null;

            foreach (GameObject go in backgrounds)
            {
                distance = Vector3.Distance(go.transform.position, cam.transform.position);

                if (distance > previousDistance)
                {
                    previousDistance = distance;
                    objectToReturn = go;
                }
            }
            ReturnToPool(objectToReturn);
        }
        lastBackground = GetNextObject().transform;
        lastBackground.position = spawnPos;
        lastRenderer = lastBackground.GetComponent<Renderer>();
        lastRenderer.sortingOrder = 0;
    }

    private void UpdateNextSpawnTrigger()
    {
        // Spawn again after camera reaches (new) last background's right edge
        nextSpawnAtCamRightX = lastRenderer.bounds.max.x + xBuffer;
    }

    public void Reset()
    {
        foreach (GameObject background in backgrounds)
        {
            ReturnToPool(background);
        }
    }
}
