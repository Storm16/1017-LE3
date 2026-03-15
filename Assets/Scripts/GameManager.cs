using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState
{
    InMenu,

    InGame,

    GameOver,
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameState CurrentGameState { get; private set; }
    private SoundManager soundManager;
    public SoundManager SoundManager
    {
        get
        {
            if (soundManager == null)
            {
                soundManager = FindFirstObjectByType<SoundManager>();
            }
            return soundManager;
        }
        private set
        {
            soundManager = value;
        }
    }

    private SegmentSpawner segmentSpawner;
    public SegmentSpawner SegmentSpawner
    {
        get
        {
            if (segmentSpawner == null)
            {
                segmentSpawner = FindFirstObjectByType<SegmentSpawner>();
            }
            return segmentSpawner;
        }
        private set
        {
            segmentSpawner = value;
        }
    }

    private PlayerController player;
    public PlayerController Player
    {
        get
        {
            if (player == null)
            {
                player = FindFirstObjectByType<PlayerController>();
            }
            return player;
        }
        private set
        {
            player = value;
        }
    }

    private UIManager uiManager;
    public UIManager UIManager
    {
        get
        {
            if (uiManager == null)
            {
                uiManager = FindFirstObjectByType<UIManager>();
            }
            return uiManager;
        }
        private set
        {
            uiManager = value;
        }
    }

    private BackgroundManager backgroundManager;
    public BackgroundManager BackgroundManager
    {
        get
        {
            if (backgroundManager == null)
            {
                backgroundManager = FindFirstObjectByType<BackgroundManager>();
            }
            return backgroundManager;
        }
        private set
        {
            backgroundManager = value;
        }
    }

    private TimerController Timer;

    public TimerController timer
    {
        get
        {
            if (Timer == null)
            {
                Timer = FindFirstObjectByType<TimerController>();
            }
            return Timer;
        }
        private set
        {
            Timer = value;
        }
    }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject); // Ensure only one GameManager persists
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);  // Ensure GameManager persists across scenes
        Debug.Log("GameManager is set to DontDestroyOnLoad");
    }

    private void OnEnable()
    {
        SceneManager.activeSceneChanged += SceneLoaded;
    }

    private void SceneLoaded(Scene oldScene, Scene newScene)
    {
        Debug.Log("Scene changed");
    }

    private void Start()
    {
        //SceneManager.sceneLoaded
        CurrentGameState = GameState.InMenu;

        Debug.Log("Current Game State at Start: " + CurrentGameState);
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("MainScene");

        CurrentGameState = GameState.InMenu;
    }

    public void StartGame()
    {
        Debug.Log("Starting Game...");

        SetGameState(GameState.InGame);

        SegmentSpawner.Initialize();
        Player.Initialize();
        UIManager.OnPlayPressed();
        BackgroundManager.Reset();
        timer.Initialize();
    }

    public void GameOver()
    {
        Debug.Log("Game Over...");

        SetGameState(GameState.GameOver);
        SceneManager.LoadScene("GameOverScene");
        timer.Stop();
    }

    public void RestartGame()
    {
        player.Reset();
        SegmentSpawner.Reset();
        UIManager.OnResetPressed();
        timer.Reset();
        SetGameState(GameState.InMenu);
    }

    public void SetGameState(GameState state)
    {
        Debug.Log("Setting GameState: " + state);  // Log state change

        CurrentGameState = state;
    }
}