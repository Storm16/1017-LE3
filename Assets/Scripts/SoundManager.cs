using UnityEngine;

/// <summary>
///A singleten class that handles the sound for the game./
/// </summary>

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioSource musicSource, sfxSource;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void ChangeMusicVolume(float newVolume)
    {
        musicSource.volume = newVolume;
    }

    public void ChangeSFXVolume(float newVolume)
    {
        sfxSource.volume = newVolume;
    }
}
