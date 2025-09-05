using UnityEngine;
using UnityEngine.UI;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

public class CursorManager : MonoBehaviour
{
    public static CursorManager instance;

    [Header("必須参照")]
    [SerializeField] private Canvas canvas;                 // Canvas を入れる
    [SerializeField] private RectTransform cursorRoot;      // このスクリプトを付けたオブジェクトのRectTransformを入れる（= 自分）
    [SerializeField] public Image finger;
    [SerializeField] private Image pen;
    [SerializeField] private Vector2 cursorOffset = new Vector2(100f, 0f);
    public enum CursorType
    {
        None,
        Finger,
        Pen
    }

    public void SetCursor(CursorType type)
    {
        if (finger) finger.enabled = (type == CursorType.Finger);
        if (pen) pen.enabled = (type == CursorType.Pen);
    }


    private Camera CanvasCam =>
        canvas.renderMode == RenderMode.ScreenSpaceOverlay ? null : canvas.worldCamera;

    private Texture2D _blank;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(canvas.gameObject);

            // 透明カーソルを用意
            _blank = new Texture2D(1, 1, TextureFormat.RGBA32, false);
            _blank.SetPixel(0, 0, new Color(0, 0, 0, 0));
            _blank.Apply();
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void OnEnable()
    {
        Cursor.lockState = CursorLockMode.None;       // 必要に応じて Confined / Locked に
        Cursor.visible = false;
        // 透明化（OS に描かれても見えなくする）
        Cursor.SetCursor(_blank, Vector2.zero, CursorMode.Auto);
    }

    private void OnDisable()
    {
        // 復帰
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    private void Start()
    {
        finger.enabled = false;
        pen.enabled = false;

        // カーソルUIが他のUIに隠れないよう最前面へ
        cursorRoot.SetAsLastSibling();

        // クリック等のブロッキング回避
        finger.raycastTarget = false;
        pen.raycastTarget = false;
    }

    private void LateUpdate()
    {
        Vector2 screenPos = ReadMousePosition();
        RectTransform canvasRect = canvas.transform as RectTransform;

        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
                canvasRect, screenPos, CanvasCam, out var localPos))
        {
            // マウス座標 + オフセット
            cursorRoot.anchoredPosition = localPos + cursorOffset;
        }
        
        cursorRoot.SetAsLastSibling();

        if (Cursor.visible)
        {
            Cursor.visible = false;
        }
    }


    private Vector2 ReadMousePosition()
    {
#if ENABLE_INPUT_SYSTEM
        if (Mouse.current != null)
            return Mouse.current.position.ReadValue();
#endif
        return Input.mousePosition;
    }
    
    
}
