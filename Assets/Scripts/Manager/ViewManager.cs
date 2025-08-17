using System.Collections.Generic;
using UnityEngine;

public class ViewManager : MonoBehaviour
{
    [SerializeField] public StageInfo[] Stages;
    [SerializeField] public GameObject playerCapsule;
    [SerializeField] public StrokeManager2D StrokeManager2D;
    [SerializeField] public StrokeManager3D StrokeManager3D;
    [SerializeField] public GameObject camera2D;
    [SerializeField] public GameObject camera3D;

    [System.Serializable]
    public class StageInfo
    {
        [Header("2Dステージと3Dステージ")]
        public GameObject mazeCanvas;
        public GameObject mazeCubes;
        [Header("プレイヤーの初期位置・角度")]
        public Vector3 playerPosition;
        public Vector3 playerRotation;
        [Header("制限時間（秒）")]
        public float limitTime2D;
        public float limitTime3D;
        [Header("動くオブジェクトなど")]
        public ResetObject[] resetObjects;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// 全ステージのGameObjectのsetActiveを全部falseにする
    /// </summary>
    public void InitializeStages()
    {
        //ステージのsetActiveを全部falseに
        foreach (StageInfo s in Stages)
        {
            s.mazeCanvas.SetActive(false);
            s.mazeCubes.SetActive(false);
            foreach (ResetObject r in s.resetObjects)
            {
                r.gameObject.SetActive(false);
            }
        }
        playerCapsule.transform.position = Vector3.zero;
        playerCapsule.SetActive(false);
        StrokeManager2D.gameObject.SetActive(false);
        StrokeManager3D.gameObject.SetActive(false);
        camera2D.SetActive(false);
        camera3D.SetActive(false);
        GameManager.elapsedTime = 0;
        GameManager.isWaiting = false;
    }
}
