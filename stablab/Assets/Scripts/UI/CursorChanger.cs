using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorChanger : MonoBehaviour
{
    public Texture2D newCursor;
    private Vector2 cursorHotspot;
    public void Start()
    {
        // Create a texture. Texture size does not matter, since
        // LoadImage will replace with with incoming image size.
        cursorHotspot = new Vector2(newCursor.width / 2, newCursor.height / 2);
    }

    public void OnMouseEnter()
    {
        Cursor.SetCursor(newCursor, cursorHotspot, CursorMode.Auto);
    }

    public void OnMouseExit()
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }
}
