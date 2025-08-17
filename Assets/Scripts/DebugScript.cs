using System.Collections;
using UnityEngine;
using static UnityEngine.CullingGroup;

public class DebugScript : MonoBehaviour
{
    [Tooltip("Pキーでポーズするかどうか"), SerializeField] private bool allowPause = true;
    [Tooltip("Pキーでポーズするかどうか"), SerializeField] private StageChanger stageChanger;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        checkPause();
        checkRestart();
    }

    void checkPause()
    {
        if (Input.GetKeyDown(KeyCode.P) && allowPause)
        {
            //Pキーの入力でpauseを切り替え
            GameManager.isPausing = !GameManager.isPausing;
        }

    }

    void checkRestart()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            //Rキーの入力でリセット
            stageChanger.ChangeStages(GameManager.nowStage, GameManager.now2Dor3D);
        }
    }
}
