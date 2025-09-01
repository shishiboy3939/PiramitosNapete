using UnityEngine;
using System.Collections.Generic;
public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    [SerializeField] public AudioClip SE_SceneMovie;
    [SerializeField] public AudioClip SE_FootStep;
    [SerializeField] public AudioClip SE_EatSoul;
    [SerializeField] public AudioClip SE_breakDice;
    [SerializeField] public AudioClip SE_Appear2DMap;
    [SerializeField] public AudioClip SE_Pencil;
    [SerializeField] public AudioClip SE_GetAnk;
    [SerializeField] public AudioClip SE_Respawn;
    [SerializeField] public AudioClip SE_SpeedUp;
    [SerializeField] public AudioClip SE_Into_PopIn;
    [SerializeField] public AudioClip SE_Into_UnLock;
    [SerializeField] public AudioClip SE_ChangeIcon;
    [SerializeField] public AudioClip LongSE_Clock;
    [SerializeField] public AudioClip Ending;
    [SerializeField] public AudioClip TitleBGM;
    [SerializeField] public AudioClip Stage01BGM;
    [SerializeField] public AudioClip Stage02BGM;
    [SerializeField] public AudioClip Stage03BGM;
    [SerializeField] public AudioClip SE_GameStart;
    [SerializeField] public AudioClip MapBGM;
    [SerializeField] public AudioSource BgmSource, SeSource, LongSESorce, PencilSESorce;
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
    public void PlayLongSE(AudioClip longSE)
    {
        LongSESorce.clip = longSE;
        LongSESorce.Play();
    }
    public void PauseLongSE(AudioClip longSE)
    {
        LongSESorce.clip = longSE;
        LongSESorce.Pause();
    }
    public void ResumeLongSE()
    {
        LongSESorce.UnPause();
    }
    public void StopLongSE()
    {
        LongSESorce.Stop();
    }
    public void PlayPencilSound(AudioClip pencilSE)
    {
        PencilSESorce.PlayOneShot(pencilSE);
    }
    public void StopPencilSound()
    {
        PencilSESorce.Stop();
    }

}
