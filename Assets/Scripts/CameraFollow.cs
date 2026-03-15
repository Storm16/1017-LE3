using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private GameObject Player;

    private float fixedY, fixedZ;
    private float xOffset;

    private void Start()
    {
        fixedY = transform.position.y;
        fixedZ = transform.position.z;

        xOffset = transform.position.x;

        if (Player == null)
        {
            Player = FindFirstObjectByType<PlayerController>().gameObject;
        }
    }

    private void LateUpdate()
    {
        if (Player == null) return;

        float newXPosition = Player.transform.position.x + xOffset;

        transform.position = new Vector3(newXPosition, fixedY, fixedZ);
    }
}