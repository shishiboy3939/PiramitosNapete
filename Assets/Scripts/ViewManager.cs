using UnityEngine;

public class ViewManager : MonoBehaviour
{
    [SerializeField] GameObject mazeCanvas;
    [SerializeField] GameObject mazeCubes;
    [SerializeField] GameObject playerCapsule;
    [SerializeField] GameObject camera2D;
    [SerializeField] GameObject camera3D;

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
            mazeCubes.SetActive(false);
            playerCapsule.SetActive(false);
            camera2D.SetActive(true);
            camera3D.SetActive(false);
        }
        else
        {
            mazeCanvas.SetActive(false);
            mazeCubes.SetActive(true);
            playerCapsule.SetActive(true);
            camera2D.SetActive(false);
            camera3D.SetActive(true);
        }
    }
}
