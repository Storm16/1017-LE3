using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject playButton, resetButton, gameOverButton;

    private void Start()
    {
        Initialize();
    }

    public void Initialize()
    {
        playButton.SetActive(true);
        resetButton.SetActive(false);
        gameOverButton.SetActive(false);

        // TimerUI.SetActive(false);
    }

    public void OnPlayPressed()
    {
        playButton.SetActive(false);
        resetButton.SetActive(true);
        gameOverButton.SetActive(true);

        // TimerUI.SetActive(true);
    }

    public void OnResetPressed()
    {
        Initialize();
    }
}
