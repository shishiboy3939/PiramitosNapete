using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    [SerializeField] public AudioClip SE_SceneMovie;
    [SerializeField] public AudioClip SE_FootStep;
    [SerializeField] public AudioClip SE_EatSoul;
    [SerializeField] public AudioClip TitleBGM;
    [SerializeField] public AudioClip Stage01BGM;
    [SerializeField] public AudioClip Stage02BGM;
    [SerializeField] public AudioClip Stage03BGM;
    [SerializeField] public AudioClip MapBGM;
    [SerializeField] public AudioSource BgmSource, SeSource, FootStepSource;
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
    public void PlaySoundEffect(AudioClip sound)
    {
        SeSource.PlayOneShot(sound);
    }
    public void PlayBgm(AudioClip bgm)
    {
        BgmSource.clip = bgm;
        BgmSource.Play();
    }
    public void FootStepPlay(AudioClip footstep)
    {
        FootStepSource.clip = footstep;
        FootStepSource.Play();
    }
    public void FootStepStop()
    {
        FootStepSource.Stop();
    }
}
