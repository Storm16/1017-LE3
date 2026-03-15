using UnityEngine.UI;
using UnityEngine;

[RequireComponent(typeof(Slider))]
public class SliderAudioController : MonoBehaviour
{
    [SerializeField] private ESoundType soundType;
    private Slider slider;

    private void Awake()
    {
        slider = GetComponent<Slider>();
    }

    private void Start()
    {
        switch (soundType)
        {
            case ESoundType.Music:
                slider.value = SoundManager.Instance.GetMusicVolume();
                break;

            case ESoundType.SFX:
                slider.value = SoundManager.Instance.GetSFXVolume();
                break;
        }
    }

    private void OnEnable()
    {
        slider.onValueChanged.AddListener(ChangeSoundVolume);
    }

    private void OnDisable()
    {
        slider.onValueChanged.RemoveListener(ChangeSoundVolume);
    }

    private void ChangeSoundVolume(float newVolume)
    {
        switch (soundType)
            {
            case ESoundType.Music:
                SoundManager.Instance.ChangeMusicVolume(newVolume);
                break;

            case ESoundType.SFX:
                SoundManager.Instance.ChangeSFXVolume(newVolume);
                break;
        }
    }
}
public enum ESoundType
{
    Music,
    SFX,
    none
}
