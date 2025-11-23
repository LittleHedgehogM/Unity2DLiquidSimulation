using UnityEngine;

public class CursorController : MonoBehaviour
{
    public Texture2D cursorTexture;     // 你的 png 光标
    public Vector2 hotspot = Vector2.zero;  // 热点（点击位置）
    public CursorMode cursorMode = CursorMode.Auto;

    void Start()
    {
        Cursor.SetCursor(cursorTexture, hotspot, cursorMode);
    }
}
