using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform playerTransform;
    void Start()
    {
        playerTransform = FindFirstObjectByType<Player>().transform;
    }

    void Update()
    {
        if (playerTransform != null)
        {
            transform.position = new Vector3(playerTransform.position.x, playerTransform.position.y, transform.position.z);
        }
    }
}
