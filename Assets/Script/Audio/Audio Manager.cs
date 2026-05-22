using DG.Tweening;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    //public float musicVolume, sfxVolume;

    [Header("Audio Sources")]
    //public AudioMixer audioMixer;
    public AudioSource sfxSource;
    public AudioSource musicSource;

    [Header("Clips")]
    public AudioClip partButtonAudioClip;
    public AudioClip placeAudioClip;
    public AudioClip lockAudioClip;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }

    //private void Start()
    //{
    //    musicVolume = PlayerPrefs.GetFloat("music", 1);
    //    sfxVolume = PlayerPrefs.GetFloat("sfx", 1);

    //    SetMusicVolume(musicVolume);
    //    SetSfxVolume(sfxVolume);
    //}

    //public void SetMusicVolume(float volume)
    //{
    //    musicVolume = volume;

    //    volume = Mathf.Clamp(volume, 0.0001f, 1f);

    //    float dB = Mathf.Log10(volume) * 20;
    //    audioMixer.SetFloat("Music_Volume", dB);

    //    PlayerPrefs.SetFloat("music", volume);
    //}

    //public void SetSfxVolume(float volume)
    //{
    //    sfxVolume = volume;

    //    volume = Mathf.Clamp(volume, 0.0001f, 1f);

    //    float dB = Mathf.Log10(volume) * 20;
    //    audioMixer.SetFloat("Sfx_Volume", dB);

    //    PlayerPrefs.SetFloat("sfx", volume);
    //}

    public void PlayBGM(AudioClip clip, float duration)
    {
        if (musicSource.clip == clip)
        {
            print("Return");
            return;
        }

        if (musicSource.clip == null)
        {
            print("New Clip");
            musicSource.clip = clip;
        }

        FadeBGM(clip, duration);
    }

    public void PlaySFX(SFXType type)
    {
        print("Play SFX");

        switch (type)
        {
            case SFXType.Part_Button:
                sfxSource.PlayOneShot(partButtonAudioClip);
                break;
            case SFXType.Place_Part:
                sfxSource.PlayOneShot(placeAudioClip);
                break;
            case SFXType.Lock:
                sfxSource.PlayOneShot(lockAudioClip);
                break;
        }
    }

    public void PlayClip(AudioClip audio)
    {
        sfxSource.PlayOneShot(audio);
    }

    public void FadeBGM(AudioClip newClip, float duration)
    {
        musicSource.DOFade(0, duration).OnComplete(() =>
        {
            musicSource.clip = newClip;
            musicSource.Play();
            musicSource.DOFade(1, duration);
        });
    }
}

public enum SFXType
{
    Part_Button,
    Place_Part,
    Lock
}