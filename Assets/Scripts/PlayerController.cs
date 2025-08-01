using UnityEngine;
using static UnityEditor.PlayerSettings;

public class PlayerController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(GameManager.gameMode == 0)
            {
                GameManager.gameMode = 1;
            }
            else
            {
                GameManager.gameMode = 0;
            }
        }
    }


}
