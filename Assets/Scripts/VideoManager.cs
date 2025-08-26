using UnityEngine;
using UnityEngine.Video;

public class VideoManager : MonoBehaviour
{
    [SerializeField] private VideoPlayer videoPlayer;
    public GameObject endingView;
    public static VideoManager Instance;
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
        endingView.SetActive(true);
        videoPlayer.Play();
        SoundManager.Instance.StopBgm();
    }

    void OnVideoEnd(VideoPlayer vp)
    {
        endingView.SetActive(false);
        Debug.Log("動画が終了しました");
        GameManager.nowStage = 0;
        StartCoroutine(ClearOrOverManager.Instance.BlackOut());
    }
}
