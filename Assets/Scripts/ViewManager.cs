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
        public GameObject mazeCanvas;
        public GameObject mazeCubes;
        public Vector3 playerPosition;
        public Vector3 playerRotation;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitializeStages()
    {
        //ステージのsetActiveを全部falseに
        foreach(StageInfo s in Stages)
        {
            s.mazeCanvas.SetActive(false);
            s.mazeCubes.SetActive(false);
        }
        playerCapsule.transform.position = Vector3.zero;
        playerCapsule.SetActive(false);
        StrokeManager2D.gameObject.SetActive(false);
        StrokeManager3D.gameObject.SetActive(false);
        camera2D.SetActive(false);
        camera3D.SetActive(false);
    }
}
