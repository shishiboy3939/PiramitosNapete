using UnityEditor;
using UnityEngine;

public class CursorManager : MonoBehaviour
{
    [SerializeField] Texture2D cursorImage;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.SetCursor(cursorImage, Vector2.zero, CursorMode.Auto);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
