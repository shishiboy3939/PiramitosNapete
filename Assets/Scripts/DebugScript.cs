using System.Collections;
using UnityEngine;
using static UnityEngine.CullingGroup;
using UnityEngine.AI;
using System.Collections.Generic;

public class DebugScript : MonoBehaviour
{
    [Tooltip("Pキーでポーズするかどうか"), SerializeField] private bool allowPause = true;
    [Tooltip("Rキーでリスタートするかどうか"), SerializeField] private bool allowRestart = true;
    [Tooltip("Tキーでタイトル画面に戻るどうか"), SerializeField] private bool allowQuickTitle = true;
    [Tooltip("ヒエラルキー上のStageChanger"), SerializeField] private StageChanger stageChanger;
    [SerializeField] List<NavMeshAgent> agents; 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        checkPause();
        checkRestart();
        checkQuickTitle();
    }

    void checkPause()
    {
        if (Input.GetKey(KeyCode.Keypad4) | Input.GetKey(KeyCode.Alpha4) && allowPause)
        {
            //Pキーの入力でpauseを切り替え
            GameManager.isPausing = !GameManager.isPausing;
        }

    }

    void checkRestart()
    {
        if (Input.GetKey(KeyCode.Keypad3) | Input.GetKey(KeyCode.Alpha3) && allowRestart)
        {
            //Rキーの入力でリセット
            stageChanger.ChangeStages(GameManager.nowStage, GameManager.now2Dor3D);
        }
    }

    void checkQuickTitle()
    {
        if (Input.GetKey(KeyCode.Keypad2) | Input.GetKey(KeyCode.Alpha2))
        {
            //Tキーの入力でタイトル画面へ
            stageChanger.GotoTitle();
        }

    }
}
