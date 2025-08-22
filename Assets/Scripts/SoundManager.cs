using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    [SerializeField] public AudioClip SE_SceneMove;
    [SerializeField] public AudioClip TitleBGM;
    [SerializeField] public AudioClip Stage01BGM;
    [SerializeField] public AudioClip Stage02BGM;
    [SerializeField] public AudioClip Stage03BGM;
    [SerializeField] public AudioClip MapBGM;
    [SerializeField] public AudioSource BgmSource, SeSource;

    public void PlaySoundEffect(AudioClip sound)
    {
        SeSource.PlayOneShot(sound);
    }
    public void PlayBgm(AudioClip bgm)
    {
        BgmSource.clip = bgm;
        BgmSource.Play();
    }
}
