using UnityEngine;

public class MouseController : MonoBehaviour
{
   [SerializeField] private Texture2D cursorTexture;

   [SerializeField] private Vector2 clickPosition=Vector2.zero;


   void Start()
   {
       Cursor.SetCursor(cursorTexture, clickPosition, CursorMode.Auto);
   }
}
