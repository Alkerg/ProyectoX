using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public LevelManager levelManager;
    Vector2 movement;
    Vector2 mousePosition;
    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    void FixedUpdate()
    {
        if(levelManager.isPaused || levelManager.isGameOver) return;

        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
        
        Vector2 lookDirection = mousePosition - rb.position;
        float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = angle;
    }
}
