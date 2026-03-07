using UnityEngine;
using UnityEngine.UI;

public enum GameManagerAction
{
    PlayGame,
    StartGame,
    GameOver,
    RestartGame,
}

public class GameManagerButton : MonoBehaviour
{
    [SerializeField] private GameManagerAction gameManagerAction;

    private void OnEnable()
    {
        GetComponent<Button>().onClick.AddListener(PerformGameManagerAction);
    }

    private void OnDisable()
    {
        GetComponent<Button>().onClick.RemoveListener(PerformGameManagerAction);
    }

    private void PerformGameManagerAction()
    {
        switch (gameManagerAction)
        {
            case GameManagerAction.PlayGame:
            GameManager.Instance.PlayGame();
            break;

            case GameManagerAction.StartGame:
            GameManager.Instance.StartGame();
            break;

            case GameManagerAction.GameOver:
            GameManager.Instance.GameOver();
            break;

            case GameManagerAction.RestartGame:
            GameManager.Instance.RestartGame();
            break;
        }
    }
}
