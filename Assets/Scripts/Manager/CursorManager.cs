using UnityEditor;
using UnityEngine;

public class CursorManager : MonoBehaviour
{
    [SerializeField] Texture2D cursorImage;
    [SerializeField] Texture2D cursorImage2;
    [SerializeField] bool switchCursor = false;
    bool pswitchCursor = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.SetCursor(cursorImage, new Vector2(0, 800), CursorMode.Auto);
    }

    // Update is called once per frame
    void Update()
    {
        //カーソル画像切り替え
        if (!pswitchCursor && switchCursor)
        {
            Cursor.SetCursor(cursorImage2, new Vector2(0, 169), CursorMode.Auto);
        }
        if (pswitchCursor && !switchCursor)
        {
            Cursor.SetCursor(cursorImage, new Vector2(0, 800), CursorMode.Auto);
        }
        pswitchCursor = switchCursor;
    }
}
