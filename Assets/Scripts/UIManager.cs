using UnityEngine;
using TMPro;
using System.Threading;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private TextMeshProUGUI timer;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer.text = ((int)Mathf.Ceil(GameManager.elapsedTime)).ToString();
    }
}
