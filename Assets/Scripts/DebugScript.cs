using System.Collections;
using UnityEngine;

public class DebugScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InvokeRepeating("RepeatMsg", 0, 1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void RepeatMsg()
    {
        Debug.Log(Input.mousePosition.x + " " + Input.mousePosition.y);
    }
}
