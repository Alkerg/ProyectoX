using UnityEngine;

public class MouseController : MonoBehaviour
{
    [SerializeField] private Texture2D cursorTexture;
    [SerializeField] private Texture2D cursorHoverTexture;
    [SerializeField] private Vector2 clickPosition = Vector2.zero;

    const string ENEMY_TAG = "Enemy";

    private bool isHoveringEnemy = false;

    void Start()
    {
        Cursor.SetCursor(cursorTexture, clickPosition, CursorMode.Auto);
    }

    void Update()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

        if (hit.collider != null && hit.collider.CompareTag(ENEMY_TAG))
        {
            if (!isHoveringEnemy)
            {
                isHoveringEnemy = true;
                Cursor.SetCursor(cursorHoverTexture, clickPosition, CursorMode.Auto);
            }
        }
        else
        {
            if (isHoveringEnemy)
            {
                isHoveringEnemy = false;
                Cursor.SetCursor(cursorTexture, clickPosition, CursorMode.Auto);
            }
        }
    }
}
