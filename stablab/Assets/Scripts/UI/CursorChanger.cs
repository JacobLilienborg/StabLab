using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorChanger : MonoBehaviour
{
    public Texture2D newCursor;
    public void Start()
    {
        // Create a texture. Texture size does not matter, since
        // LoadImage will replace with with incoming image size.
    }

    public void OnMouseEnter()
    {
        Cursor.SetCursor(newCursor, Vector2.zero, CursorMode.Auto);
    }

    public void OnMouseExit()
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }
}
