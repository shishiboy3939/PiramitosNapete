using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Start3DButton : MonoBehaviour
{
    [SerializeField] StageChanger stageChanger;
    [SerializeField] CanvasGroup a;
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
            stageChanger.ChangeStages(GameManager.nowStage, 1);
        }
    }

    //残り15秒になったらボタンをじわじわ表示
    void checkAlpha()
    {
        if(GameManager.elapsedTime < 15 && !isActive)
        {
            isActive = true;
            a.DOFade(endValue: 1f, duration: 1f);
        }
    }
}
