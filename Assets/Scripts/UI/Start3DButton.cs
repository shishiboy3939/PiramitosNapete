using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Start3DButton : MonoBehaviour
{
    [SerializeField] StageChanger stageChanger;
    [SerializeField] CanvasGroup a;
    [Tooltip("ボタンを表示する残り時間（秒）"), SerializeField] float displayTime = 15f;
    bool isActive = false;
    Image image;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        checkAlpha();
    }

    //ボタンが押されたらすぐ3D画面へ
    public void Start3DGame()
    {
        //ボタンが出現したらボタンとしての機能を開始
        if (a.alpha >= 0.9f)
        {
            StartCoroutine(ClearOrOverManager.Instance.ChangeStageTransition(GameManager.nowStage, 1));
            Debug.Log(ClearOrOverManager.Instance.fading);
            SoundManager.Instance.PlaySoundEffect(SoundManager.Instance.SE_Into_UnLock);
        }
    }

    //残り15秒になったらボタンをじわじわ表示
    void checkAlpha()
    {
        //isActiveを初期化
        if (ViewManager.Instance.Stages[GameManager.nowStage].limitTime2D - GameManager.elapsedTime < 0.1f)
        {
            isActive = false;
        }

        if(GameManager.elapsedTime < displayTime && !isActive)
        {
            SoundManager.Instance.PlaySoundEffect(SoundManager.Instance.SE_Into_PopIn);
            isActive = true;
            a.DOFade(endValue: 1f, duration: 1f);
        }
        else
        {
            if (a.alpha != 0f && !isActive)
            {
                a.DOFade(endValue: 0f, duration: 0f);
            }
        }
    }
}
