using UnityEngine;
using TMPro;

public class TimerController : MonoBehaviour
{
    public TextMeshProUGUI timerText;

    private float elapsedTime = 0f;
    private bool timerRunning = false;

    void Update()
    {
        if (timerRunning)
        {
            elapsedTime += Time.deltaTime;
            UpdateUI();
        }
    }

    public void Initialize()
    {
        elapsedTime = 0f;
        timerRunning = true;
    }

    public void Stop()
    {
        timerRunning = false;
    }

    public void Reset()
    {
        elapsedTime = 0f;
        timerRunning = false;
    }


    void UpdateUI()
    {
        int minutes = Mathf.FloorToInt(elapsedTime / 60);
        int seconds = Mathf.FloorToInt(elapsedTime % 60);

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}