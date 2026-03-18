using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIItem : MonoBehaviour
{
    [Header("表示する画像")]
    [Tooltip("表示する画像"), SerializeField] private Sprite itemSprite;
    [Header("アイテムの説明文")]
    [Multiline]
    [Tooltip("アイテムの説明文"), SerializeField] private string itemText;
    [Header("テキストを表示するgameObject")]
    [Tooltip("テキストを表示するgameObject"), SerializeField] private TextMeshProUGUI uiText;
    [Header("アイテム画像を表示するgameObject")]
    [Tooltip("アイテム画像を表示するgameObject"), SerializeField] private Image itemImage;
    [Header("拡大縮小するオブジェクト")]
    [Tooltip("未設定ならこのオブジェクト自身を使う"), SerializeField] private RectTransform scaleTarget;
    Vector3 initialScale;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (scaleTarget == null)
        {
            scaleTarget = transform as RectTransform;
        }

        if (scaleTarget != null)
        {
            initialScale = scaleTarget.localScale;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //EventTriggerでマウスが画像と重なったときに呼び出す関数
    //なんかめっちゃ沼った...
    //
    public void test()
    {
        if (scaleTarget != null)
        {
            scaleTarget.localScale = initialScale * 1.2f;
        }
        itemImage.sprite = itemSprite;
        uiText.text = itemText;
        //制限時間の減少を止める
        //画面暗くして文字を読みやすくするとかしたいけど一旦これで
        GameManager.isPausing = true;
        SoundManager.Instance.PauseLongSE(SoundManager.Instance.LongSE_Clock);
        SoundManager.Instance.PlaySoundEffect(SoundManager.Instance.SE_ChangeIcon);
    }

    public void test2()
    {
        if (scaleTarget != null)
        {
            scaleTarget.localScale = initialScale;
        }
        //制限時間の減少を再開
        //多分この書き方だとマウスを高速で動かすとバグる
        //でも今はシンプルさを優先
        GameManager.isPausing = false;
        SoundManager.Instance.ResumeLongSE();
        
    }

}
