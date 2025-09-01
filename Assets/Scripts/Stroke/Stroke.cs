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

    [Tooltip("ヒエラルキー上のStrokeManager2D"), SerializeField] StrokeManager2D StrokeManager2D;
    [Tooltip("ヒエラルキー上のStrokeManager3D"), SerializeField] StrokeManager3D StrokeManager3D;
    [Tooltip("3Dステージの中央X座標"), SerializeField] private float worldCenterX;
    [Tooltip("3Dステージの中央Z座標"), SerializeField] private float worldCenterZ;
    [Tooltip("3Dステージのブロックのサイズ"), SerializeField] private float blockSize = 4f;
    [Tooltip("2Dマップの1ブロックあたりのピクセル数"), SerializeField] private float blockPixel = 300f;
    [Tooltip("マップ画像のGameObject"), SerializeField] GameObject map;
    [Tooltip("鉛筆の効果音を流す頻度（秒）"), SerializeField] private float SEWaitTime = 0.5f;

    private bool isDrawing = false;
    private float mouseX, mouseY;
    private float mapX, mapY, mapW, mapH;
    private int scrCenterW, scrCenterH;

    private float SECountTime = 0;
    private bool isSEPlaying = false;

    public bool strokeEnable = true;

    //追加　LineRdenerer型のリスト宣言
    //List<LineRenderer> lineRenderers, lineRenderers3D;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //マップ画像の座標とサイズを取得
        mapX = map.GetComponent<RectTransform>().anchoredPosition.x;
        mapY = map.GetComponent<RectTransform>().anchoredPosition.y;
        mapW = map.GetComponent<RectTransform>().sizeDelta.x;
        mapH = map.GetComponent<RectTransform>().sizeDelta.y;
        scrCenterW = Screen.width / 2;
        scrCenterH = Screen.height / 2;
    }

    // Update is called once per frame
    void Update()
    {
        //マウスが2Dマップ内にあるとき
        if(!GameManager.isWaiting && CheckMousePosition())
        {
            //クリックされた瞬間
            if (Input.GetMouseButtonDown(0))
            {
                //lineObjを生成し、初期化する
                _addLineObject();
                _addLineObject3D();
                isDrawing = true;
            }
            //クリック中（ストローク中）
            if (Input.GetMouseButton(0))
            {
                if (isDrawing)
                {
                    _addPositionDataToLineRendererList();
                    _addPositionDataToLineRendererList3D();
                    //線を描いている間、効果音を鳴らし続ける
                    if (!isSEPlaying)
                    {
                        isSEPlaying = true;
                        SECountTime = 0;
                        SoundManager.Instance.PlayPencilSound(SoundManager.Instance.SE_Pencil);
                    }
                    SECountTime += Time.deltaTime;
                    if (SECountTime > SEWaitTime)
                    {
                        isSEPlaying = false;
                    }
                }
            }
            else
            {
                isDrawing = false;
                isSEPlaying = false;
                SECountTime = 0;
                SoundManager.Instance.StopPencilSound();
            }
        }
        else
        {
            isDrawing = false;
            isSEPlaying = false;
            SECountTime = 0;
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
        StrokeManager2D.lineRenderers2D.Add(lineObj.GetComponent<LineRenderer>());
        //lineObjを自身の子要素に設定
        lineObj.transform.SetParent(StrokeManager2D.transform);

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
        StrokeManager3D.lineRenderers3D.Add(lineObj3D.GetComponent<LineRenderer>());
        //lineObjを自身の子要素に設定
        lineObj3D.transform.SetParent(StrokeManager3D.transform);
        //lineObj初期化処理
        _initRenderers3D();
    }

    //lineObj初期化処理
    void _initRenderers()
    {
        //線をつなぐ点を0に初期化
        StrokeManager2D.lineRenderers2D.Last().positionCount = 0;
        //マテリアルを初期化
        StrokeManager2D.lineRenderers2D.Last().material = lineMaterial;
        //色の初期化
        StrokeManager2D.lineRenderers2D.Last().material.color = lineColor;
        //太さの初期化
        StrokeManager2D.lineRenderers2D.Last().startWidth = lineWidth;
        StrokeManager2D.lineRenderers2D.Last().endWidth = lineWidth;
        StrokeManager2D.lineRenderers2D.Last().textureMode = LineTextureMode.Tile;
    }

    void _initRenderers3D()
    {
        //線をつなぐ点を0に初期化
        StrokeManager3D.lineRenderers3D.Last().positionCount = 0;
        //マテリアルを初期化
        StrokeManager3D.lineRenderers3D.Last().material = lineMaterial;
        //色の初期化
        StrokeManager3D.lineRenderers3D.Last().material.color = lineColor;
        //太さの初期化
        StrokeManager3D.lineRenderers3D.Last().startWidth = lineWidth*10;
        StrokeManager3D.lineRenderers3D.Last().endWidth = lineWidth*10;
        StrokeManager3D.lineRenderers3D.Last().textureMode = LineTextureMode.Tile;
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
        StrokeManager2D.lineRenderers2D.Last().transform.localPosition = localPosition;

        //lineObjの線と線をつなぐ点の数を更新
        StrokeManager2D.lineRenderers2D.Last().positionCount += 1;

        //LineRendererコンポーネントリストを更新
        StrokeManager2D.lineRenderers2D.Last().SetPosition(StrokeManager2D.lineRenderers2D.Last().positionCount - 1, worldPosition);

        //あとから描いた線が上に来るように調整
        StrokeManager2D.lineRenderers2D.Last().sortingOrder = StrokeManager2D.lineRenderers2D.Count;
    }

    void _addPositionDataToLineRendererList3D()
    {
        //マウスのスクリーン座標から3Dモード時のワールド座標に変換
        Vector3 worldPosition3D = new Vector3(worldCenterX + (mouseX - scrCenterW - mapX) / blockPixel * blockSize, 0.5f, worldCenterZ + (mouseY - scrCenterH - mapY) / blockPixel * blockSize);

        //ワールド座標をローカル座標に変換
        Vector3 localPosition3D = transform.InverseTransformPoint(worldPosition3D.x, worldPosition3D.y, -1.0f);

        //lineRenderersの最後のlineObjのローカルポジションを上記のローカルポジションに設定
        StrokeManager3D.lineRenderers3D.Last().transform.localPosition = localPosition3D;

        //lineObjの線と線をつなぐ点の数を更新
        StrokeManager3D.lineRenderers3D.Last().positionCount += 1;

        //LineRendererコンポーネントリストを更新
        StrokeManager3D.lineRenderers3D.Last().SetPosition(StrokeManager3D.lineRenderers3D.Last().positionCount - 1, worldPosition3D);

        //あとから描いた線が上に来るように調整
        StrokeManager3D.lineRenderers3D.Last().sortingOrder = StrokeManager3D.lineRenderers3D.Count;
    }

    //マウス座標が2Dマップの中かどうか
    bool CheckMousePosition()
    {
        Vector3 m = Input.mousePosition;
        mouseX = m.x;
        mouseY = m.y;
        if (mouseX - scrCenterW - mapX < mapW / 2 &&
            mouseX - scrCenterW - mapX > -mapW / 2 &&
            mouseY - scrCenterH - mapY < mapH / 2 &&
            mouseY - scrCenterH - mapY > -mapH / 2)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
