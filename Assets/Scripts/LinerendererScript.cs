using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinerendererScript : MonoBehaviour
{
    LineRenderer linerend;

    void Start()
    {
        linerend = gameObject.AddComponent<LineRenderer>();

        Vector3[] positions = new Vector3[]{
        new Vector3(0, -3, 0),
        new Vector3(3, 3, 0),
        new Vector3(-1, 4, 0),
        };

        // 点の数を指定する
        linerend.positionCount = positions.Length;

        //マテリアルの設定
        linerend.material = new Material(Shader.Find("Sprites/Default"));
        //色を指定する
        linerend.startColor = Color.blue;
        linerend.endColor = Color.red;

        // 線を引く場所を指定する
        linerend.SetPositions(positions);
    }
}