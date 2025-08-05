using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class PlayerController : MonoBehaviour
{
    [SerializeField] ViewManager viewManager;
    private int _stage = 0;
    private int _2Dor3D = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ChangeStages(0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        CheckStageChange();
    }

    void CheckStageChange()
    {
        //スペースキーを押したらステージを切り替える
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //切り替える先のステージを決める
            if (GameManager.now2Dor3D == 0)
            {
                //2Dのとき
                _stage = GameManager.nowStage;
                _2Dor3D = 1;
            }
            else
            {
                //3Dのとき
                if (GameManager.nowStage == 2)
                {
                    _stage = 0;
                }
                else
                {
                    _stage++;
                }
                _2Dor3D = 0;
            }
            
            ChangeStages(_stage, _2Dor3D);
        }
    }

    void ChangeStages(int stage, int dim)
    {
        viewManager.InitializeStages();
        if(dim == 0)
        {
            //2Dのとき
            viewManager.Stages[stage].mazeCanvas.SetActive(true);
            //線を全部消す
            foreach (Transform n in viewManager.StrokeManager2D.transform)
            {
                Destroy(n.gameObject);
            }
            viewManager.StrokeManager2D.lineRenderers2D = new List<LineRenderer>();
            foreach (Transform n in viewManager.StrokeManager3D.transform)
            {
                Destroy(n.gameObject);
            }
            viewManager.StrokeManager3D.lineRenderers3D = new List<LineRenderer>();
            viewManager.StrokeManager2D.gameObject.SetActive(true);
            viewManager.camera2D.SetActive(true);
        }
        else
        {
            //3Dのとき
            viewManager.Stages[stage].mazeCubes.SetActive(true);
            //プレイヤーを指定座標に配置
            viewManager.playerCapsule.transform.position = viewManager.Stages[stage].playerPosition;
            viewManager.playerCapsule.transform.localEulerAngles = viewManager.Stages[stage].playerRotation;
            viewManager.playerCapsule.SetActive(true);
            viewManager.StrokeManager3D.gameObject.SetActive(true);
            viewManager.camera3D.SetActive(true);
        }
        GameManager.nowStage = stage;
        GameManager.now2Dor3D = dim;
    }
}
