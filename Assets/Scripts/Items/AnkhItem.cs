using UnityEngine;
using static UnityEditor.Timeline.Actions.MenuPriority;

public class AnkhItem : MonoBehaviour
{
    [Tooltip("ヒエラルキー上のPlayerCapsuleオブジェクト"), SerializeField] GameObject player;
    [Tooltip("ヒエラルキー上のStageChanger"), SerializeField] private StageChanger stageChanger;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //プレイヤーと衝突したら、ステージをリスタート（制限時間は満タンで）
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.name == player.name)
        {
            AnkhRestart();
            //リスタートしたら、自身のsetActiveをfalseにして、リスタート後に再度現れないようにする
            //ただこの処理だと、1ステージにアンクを1つしか配置できない
            //もしアンクを複数配置したいなら、また処理を考えないといけない
            gameObject.SetActive(false);
        }
    }

    //ただ今のステージをリスタートするだけ
    void AnkhRestart()
    {
        stageChanger.ChangeStages(GameManager.nowStage, GameManager.now2Dor3D);
    }

}
