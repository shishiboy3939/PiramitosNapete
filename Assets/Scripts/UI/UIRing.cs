using DG.Tweening;
using UnityEngine;

public class UIRing : MonoBehaviour
{
    [Tooltip("ヒエラルキー上のAmountMap"), SerializeField] private GameObject amountMap;
    float mapX, mapY;
    float offset = -80f;
    float space = 60;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        SetUIPosition();
    }

    void SetUIPosition()
    {
        //GameManager.nowStageによってRingの位置を変える
        //ステージは0～2まで
        mapX = amountMap.GetComponent<RectTransform>().localPosition.x;
        mapY = amountMap.GetComponent<RectTransform>().localPosition.y + offset + space * GameManager.nowStage;
        GetComponent<RectTransform>().localPosition = new Vector3(mapX, mapY, 0);
    }
}
