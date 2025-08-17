using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ClearOrOverManager : MonoBehaviour
{
    [SerializeField] ViewManager viewManager;
    [SerializeField] StageChanger stageChanger;
    [SerializeField] GameObject clearImage;
    private int _stage = 0;
    private int _2Dor3D = 0;
    private bool clearStarted = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        checkElapsedTime();
    }

    void checkElapsedTime()
    {
        //GameManager.isWaitingがfalseのとき、経過時間を計測して時間切れか判定する
        if (!GameManager.isWaiting)
        {
            if (GameManager.now2Dor3D == 0)
            {
                //2Dのとき
                if (GameManager.elapsedTime <= 0)
                {
                    //制限時間を超えたら3Dステージへ移動
                    stageChanger.ChangeStages(GameManager.nowStage, 1);
                }
            }
            else
            {
                //3Dのとき
                if (GameManager.elapsedTime <= 0)
                {
                    //制限時間を超えたらゲームオーバー
                    GameOver();
                }
            }
            //ポーズ状態じゃなければタイマーを動かす
            if(!GameManager.isPausing)
            {
                GameManager.elapsedTime -= Time.deltaTime;
            }
        }
    }

    public void StageClear()
    {
        //クリア演出のコルーチンを実行
        if(!clearStarted)
        {
            StartCoroutine(ClearEffect());
            clearStarted = true;
        }
        
    }

    public void GameOver()
    {
        //同じステージの2D画面に戻る
        stageChanger.ChangeStages(GameManager.nowStage, 0);
    }

    public IEnumerator ClearEffect() {
        //クリア画像表示
        GameManager.isWaiting = true;
        yield return new WaitForSeconds(1);
        var image = clearImage.GetComponent<Image>();
        image.DOFade(1, 2);
        yield return new WaitForSeconds(5);

        //次のステージの2D画面に進む
        if (GameManager.nowStage == 2)
        {
            _stage = 0;
        }
        else
        {
            _stage++;
        }
        _2Dor3D = 0;
        stageChanger.ChangeStages(_stage, _2Dor3D);
        //初期化
        image.DOFade(0, 0);
        clearStarted = false;
    }

    
}
