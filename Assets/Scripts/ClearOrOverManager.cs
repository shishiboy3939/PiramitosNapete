using UnityEngine;

public class ClearOrOverManager : MonoBehaviour
{
    [SerializeField] ViewManager viewManager;
    [SerializeField] PlayerController playerController;
    private int _stage = 0;
    private int _2Dor3D = 0;
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
        //経過時間を計測
        if (GameManager.now2Dor3D == 0)
        {
            //2Dのとき
            if (GameManager.elapsedTime > viewManager.Stages[GameManager.nowStage].limitTime2D)
            {
                //制限時間を超えたら3Dステージへ移動
                playerController.ChangeStages(GameManager.nowStage, 1);
            }
        }
        else
        {
            //3Dのとき
            if (GameManager.elapsedTime > viewManager.Stages[GameManager.nowStage].limitTime3D)
            {
                //制限時間を超えたらゲームオーバー
                GameOver();
            }
        }
        GameManager.elapsedTime += Time.deltaTime;
    }

    public void StageClear()
    {
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
        playerController.ChangeStages(_stage, _2Dor3D);
    }
    void GameOver()
    {
        //同じステージの2D画面に戻る
        playerController.ChangeStages(GameManager.nowStage, 0);
    }
}
