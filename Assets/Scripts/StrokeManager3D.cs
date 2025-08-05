using System.Collections.Generic;
using UnityEngine;

public class StrokeManager3D : MonoBehaviour
{
    public List<LineRenderer> lineRenderers3D;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        lineRenderers3D = new List<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
