using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;

public class VideoManager : MonoBehaviour
{
    [SerializeField] private VideoPlayer videoPlayer;
    [SerializeField] GameObject clearImage;
    public GameObject endingView;
    public static VideoManager Instance;
    [SerializeField] private Tutorialmanager tutorialmanager;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        videoPlayer = GetComponent<VideoPlayer>();
        // 動画終了時に呼ばれるイベントを登録
        videoPlayer.loopPointReached += OnVideoEnd;
        endingView.SetActive(false);
    }
    public void EndingPlay()
    {
        //2D画面が一瞬映るのを防ぐ為に黒画面を一瞬映してる
        var image = clearImage.GetComponent<Image>();
        endingView.SetActive(true);
        videoPlayer.Play();
        image.DOFade(0, 1);
        SoundManager.Instance.PlaySoundEffect(SoundManager.Instance.Ending);
        SoundManager.Instance.StopLongSE();
        tutorialmanager.SetEnabled(true);
    }

    void OnVideoEnd(VideoPlayer vp)
    {
        endingView.SetActive(false);
        Debug.Log("動画が終了しました");
        GameManager.nowStage = 0;
        StartCoroutine(ClearOrOverManager.Instance.BlackOut());
        tutorialmanager.SetEnabled(false);
    }
}
