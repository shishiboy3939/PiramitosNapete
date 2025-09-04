using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CursorManager : MonoBehaviour {
    public static CursorManager instance;

    [SerializeField] private Image finger,pen;
    public bool displayed;

    private void Awake() {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }
    }

    private void Update() {
        transform.position = Input.mousePosition;

        if (GameManager.isWaiting)
        {
            finger.enabled = false;
            pen.enabled = true;
        }
        else
        {
            finger.enabled = true;
            pen.enabled = false;
        }

    }
}