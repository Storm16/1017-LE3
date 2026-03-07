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
        ChangeSoundVolume(slider.value);
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
                GameManager.Instance.SoundManager.ChangeMusicVolume(newVolume);
                break;

            case ESoundType.SFX:
                GameManager.Instance.SoundManager.ChangeSFXVolume(newVolume);
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
