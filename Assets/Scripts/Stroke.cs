using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Stroke : MonoBehaviour
{
    //線の材質
    [SerializeField] Material lineMaterial;
    //線の色
    [SerializeField] Color lineColor;
    //線の太さ
    [Range(0.01f, 0.5f)]
    [SerializeField] float lineWidth;

    [SerializeField] GameObject MazeCubes;
    [SerializeField] private float worldCenterX;
    [SerializeField] private float worldCenterY;
    [SerializeField] private float blockSize = 4f;
    [SerializeField] private float blockPixel = 300f;

    //追加　LineRdenerer型のリスト宣言
    List<LineRenderer> lineRenderers, lineRenderers3D;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //追加　Listの初期化
        lineRenderers = new List<LineRenderer>();
        lineRenderers3D = new List<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //lineObjを生成し、初期化する
            _addLineObject();
            _addLineObject3D();
        }

        //追加　クリック中（ストローク中）
        if (Input.GetMouseButton(0))
        {
            _addPositionDataToLineRendererList();
            _addPositionDataToLineRendererList3D();
        }
    }

    //追加　クリックしたら発動
    void _addLineObject()
    {
        //空のゲームオブジェクト作成
        GameObject lineObj = new GameObject();
        //オブジェクトの名前をStrokeに変更
        lineObj.name = "Stroke";
        //lineObjにLineRendereコンポーネント追加
        lineObj.AddComponent<LineRenderer>();
        //lineRendererリストにlineObjを追加
        lineRenderers.Add(lineObj.GetComponent<LineRenderer>());
        //lineObjを自身の子要素に設定
        lineObj.transform.SetParent(transform);

        //lineObj初期化処理
        _initRenderers();
    }

    void _addLineObject3D()
    {
        //空のゲームオブジェクト作成
        GameObject lineObj3D = new GameObject();
        //オブジェクトの名前をStrokeに変更
        lineObj3D.name = "Stroke3D";
        //lineObjにLineRendereコンポーネント追加
        lineObj3D.AddComponent<LineRenderer>();
        //lineRendererリストにlineObjを追加
        lineRenderers3D.Add(lineObj3D.GetComponent<LineRenderer>());
        //lineObjを自身の子要素に設定
        lineObj3D.transform.SetParent(MazeCubes.transform);
        //lineObj初期化処理
        _initRenderers3D();
    }

    //lineObj初期化処理
    void _initRenderers()
    {
        //線をつなぐ点を0に初期化
        lineRenderers.Last().positionCount = 0;
        //マテリアルを初期化
        lineRenderers.Last().material = lineMaterial;
        //色の初期化
        lineRenderers.Last().material.color = lineColor;
        //太さの初期化
        lineRenderers.Last().startWidth = lineWidth;
        lineRenderers.Last().endWidth = lineWidth;
    }

    void _initRenderers3D()
    {
        //線をつなぐ点を0に初期化
        lineRenderers3D.Last().positionCount = 0;
        //マテリアルを初期化
        lineRenderers3D.Last().material = lineMaterial;
        //色の初期化
        lineRenderers3D.Last().material.color = lineColor;
        //太さの初期化
        lineRenderers3D.Last().startWidth = lineWidth*10;
        lineRenderers3D.Last().endWidth = lineWidth*10;
    }

    void _addPositionDataToLineRendererList()
    {
        //マウスポインタがあるスクリーン座標を取得
        Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 1.0f);

        //スクリーン座標をワールド座標に変換
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);

        //ワールド座標をローカル座標に変換
        Vector3 localPosition = transform.InverseTransformPoint(worldPosition.x, worldPosition.y, -1.0f);

        //lineRenderersの最後のlineObjのローカルポジションを上記のローカルポジションに設定
        lineRenderers.Last().transform.localPosition = localPosition;

        //lineObjの線と線をつなぐ点の数を更新
        lineRenderers.Last().positionCount += 1;

        //LineRendererコンポーネントリストを更新
        lineRenderers.Last().SetPosition(lineRenderers.Last().positionCount - 1, worldPosition);

        //あとから描いた線が上に来るように調整
        lineRenderers.Last().sortingOrder = lineRenderers.Count;
    }

    void _addPositionDataToLineRendererList3D()
    {
        //マウスのスクリーン座標から3Dモード時のワールド座標に変換
        Vector3 worldPosition3D = new Vector3(worldCenterX + (Input.mousePosition.x-960f) / blockPixel * blockSize, 0.5f, worldCenterY + (Input.mousePosition.y - 540f) / blockPixel * blockSize);

        //ワールド座標をローカル座標に変換
        Vector3 localPosition3D = transform.InverseTransformPoint(worldPosition3D.x, worldPosition3D.y, -1.0f);

        //lineRenderersの最後のlineObjのローカルポジションを上記のローカルポジションに設定
        lineRenderers3D.Last().transform.localPosition = localPosition3D;

        //lineObjの線と線をつなぐ点の数を更新
        lineRenderers3D.Last().positionCount += 1;

        //LineRendererコンポーネントリストを更新
        lineRenderers3D.Last().SetPosition(lineRenderers3D.Last().positionCount - 1, worldPosition3D);

        //あとから描いた線が上に来るように調整
        lineRenderers3D.Last().sortingOrder = lineRenderers3D.Count;
    }
}
