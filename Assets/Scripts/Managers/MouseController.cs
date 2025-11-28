using UnityEngine;

public class MouseController : MonoBehaviour
{
   [SerializeField] private Texture2D cursorTexture;

   [SerializeField] private Vector2 clickPosition = Vector2.zero;


   public void SetCustomCursor()
   {
       Cursor.SetCursor(cursorTexture, clickPosition, CursorMode.Auto);
   }

   public void ResetCursor()
    {
        Cursor.SetCursor(null, clickPosition, CursorMode.Auto);
    }
}
