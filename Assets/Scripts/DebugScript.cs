using System.Collections;
using UnityEngine;

public class DebugScript : MonoBehaviour
{
    [Tooltip("Pキーでポーズするかどうか"), SerializeField] private bool allowPause = true;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        checkPause();
    }

    void checkPause()
    {
        if (Input.GetKeyDown(KeyCode.P) && allowPause)
        {
            //Pキーの入力でpauseを切り替え
            GameManager.isPausing = !GameManager.isPausing;
        }

    }
}
