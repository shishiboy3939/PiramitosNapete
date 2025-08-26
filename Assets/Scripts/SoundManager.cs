using UnityEngine;
using System.Collections.Generic;
public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    [SerializeField] public AudioClip SE_SceneMovie;
    [SerializeField] public AudioClip SE_FootStep;
    [SerializeField] public AudioClip SE_EatSoul;
    [SerializeField] public AudioClip SE_breakDice;
    [SerializeField] public AudioClip SE_AppearKey;
    [SerializeField] public AudioClip SE_Appear2DMap;
    [SerializeField] public AudioClip TitleBGM;
    [SerializeField] public AudioClip Stage01BGM;
    [SerializeField] public AudioClip Stage02BGM;
    [SerializeField] public AudioClip Stage03BGM;
    [SerializeField] public AudioClip MapBGM;
    [SerializeField] public AudioSource BgmSource, SeSource;
    [SerializeField] public List<AudioSource> FootStepSource;
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
    public void StopBgm()
    {
        BgmSource.Stop();
    }
    public void FootStepPlay(AudioClip footstep)
    {
        FootStepSource[GameManager.nowStage].clip = footstep;
        FootStepSource[GameManager.nowStage].Play();
    }
    public void FootStepStop()
    {
        FootStepSource[GameManager.nowStage].Stop();
    }
}
