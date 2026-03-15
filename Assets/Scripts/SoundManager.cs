using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [SerializeField] private AudioSource musicSource, sfxSource;

    private const string MUSIC_KEY = "MusicVolume";
    private const string SFX_KEY = "SFXVolume";

    private void Awake()
    {
        // Singleton check
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        // Load saved values
        float music = PlayerPrefs.GetFloat(MUSIC_KEY, 1f);
        float sfx = PlayerPrefs.GetFloat(SFX_KEY, 1f);

        musicSource.volume = music;
        sfxSource.volume = sfx;
    }

    public void ChangeMusicVolume(float newVolume)
    {
        musicSource.volume = newVolume;
        PlayerPrefs.SetFloat(MUSIC_KEY, newVolume);
    }

    public void ChangeSFXVolume(float newVolume)
    {
        sfxSource.volume = newVolume;
        PlayerPrefs.SetFloat(SFX_KEY, newVolume);
    }

    public float GetMusicVolume() => musicSource.volume;
    public float GetSFXVolume() => sfxSource.volume;
}