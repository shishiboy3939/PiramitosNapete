using System;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class StageChanger : MonoBehaviour
{
    [SerializeField] ViewManager viewManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ChangeStages(0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //ステージを変える
    public void ChangeStages(int stage, int dim)
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
            GameManager.elapsedTime = viewManager.Stages[stage].limitTime2D;
        }
        else
        {
            //3Dのとき
            viewManager.Stages[stage].mazeCubes.SetActive(true);
            //3Dステージの線をステージ標高に沿った高さに
            //標高を測りたいオブジェクトにはGroundタグを付けて！！！
            viewManager.StrokeManager3D.SetupStrokeHeight(viewManager, stage);
            //ステージプレハブを全てアクティブに
            // 親を含む子オブジェクトを再帰的に取得
            // trueを指定しないと非アクティブなオブジェクトを取得できないことに注意
            var parentAndChildren = viewManager.Stages[stage].mazeCubes.transform.GetComponentsInChildren<Transform>(true);
            var children = new Transform[parentAndChildren.Length - 1];
            // 親を除く子オブジェクトを結果にコピー
            Array.Copy(parentAndChildren, 1, children, 0, children.Length);
            foreach (Transform n in children)
            {
                n.gameObject.SetActive(true);
            }
            //プレイヤーを指定座標に配置
            viewManager.playerCapsule.transform.position = viewManager.Stages[stage].playerPosition;
            viewManager.playerCapsule.transform.localEulerAngles = viewManager.Stages[stage].playerRotation;
            viewManager.playerCapsule.SetActive(true);
            viewManager.StrokeManager3D.gameObject.SetActive(true);
            viewManager.camera3D.SetActive(true);
            GameManager.elapsedTime = viewManager.Stages[stage].limitTime3D;
        }
        GameManager.nowStage = stage;
        GameManager.now2Dor3D = dim;
    }
}
