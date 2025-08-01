using UnityEngine;

public class ViewManager : MonoBehaviour
{
    [SerializeField] GameObject mazeCanvas;
    [SerializeField] GameObject playerCapsule;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.gameMode == 0)
        {
            mazeCanvas.SetActive(true);
            playerCapsule.SetActive(false);
        }
        else
        {
            mazeCanvas.SetActive(false);
            playerCapsule.SetActive(true);
        }
    }
}
