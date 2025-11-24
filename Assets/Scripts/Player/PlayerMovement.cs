using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    float horizontalMovement;
    float verticalMovement;

    void Start()
    {
        
    }

    void Update()
    {
        horizontalMovement = Input.GetAxisRaw("Horizontal");
        verticalMovement = Input.GetAxisRaw("Vertical");
        
    }
}
