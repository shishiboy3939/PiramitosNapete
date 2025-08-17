using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;

public class StrokeManager3D : MonoBehaviour
{
    public List<LineRenderer> lineRenderers3D;

    private RaycastHit hit;
    private float maxDistance = 30;
    private float distance;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        lineRenderers3D = new List<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetupStrokeHeight(ViewManager viewManager, int stage)
    {
        //Groundタグのオブジェクトだけアクティブに
        // 親を含む子オブジェクトを再帰的に取得
        // trueを指定しないと非アクティブなオブジェクトを取得できないことに注意
        var parentAndChildren = viewManager.Stages[stage].mazeCubes.transform.GetComponentsInChildren<Transform>(true);
        var children = new Transform[parentAndChildren.Length - 1];
        // 親を除く子オブジェクトを結果にコピー
        Array.Copy(parentAndChildren, 1, children, 0, children.Length);
        foreach (Transform n in children)
        {
            if (n.gameObject.tag == "Ground")
            {
                n.gameObject.SetActive(true);
            }
            else
            {
                n.gameObject.SetActive(false);
            }

        }
        //lineRenderers3Dリストの全ての頂点を調べる
        foreach (LineRenderer n in lineRenderers3D)
        {
            for(int i = 0; i < n.positionCount; i++)
            {
                Vector3 position = n.GetPosition(i);
                //高さ10から真下にRayを飛ばし、Groundオブジェクトと衝突したときの距離を線に反映
                //標高を測りたいオブジェクトにはGroundタグを付けて！！！
                if (Physics.Raycast(new Vector3(position.x, 10, position.z), Vector3.down, out hit, maxDistance))
                {
                    n.SetPosition(i, new Vector3(position.x, 0.5f + 10 - hit.distance, position.z));
                }
                else
                {
                    n.SetPosition(i, new Vector3(position.x, 0.5f, position.z));
                }
            }
        }
    }
}
