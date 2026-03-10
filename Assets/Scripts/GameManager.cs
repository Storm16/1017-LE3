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

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
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
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("MainScene");

        CurrentGameState = GameState.InMenu;
    }

    public void StartGame()
    {
        SetGameState(GameState.InGame);

        SegmentSpawner.Initialize();
        Player.Initialize();
        UIManager.OnPlayPressed();
        BackgroundManager.Reset();
        // timer.Initialize();
    }

    public void GameOver()
    {
        SetGameState(GameState.GameOver);
        SceneManager.LoadScene("GameOverScene");
    }

    public void RestartGame()
    {
        player.Reset();
        SegmentSpawner.Reset();
        UIManager.OnResetPressed();
        // timer.Reset();
        SetGameState(GameState.InMenu);
    }

    public void SetGameState(GameState state)
    {
        CurrentGameState = state;
    }
}
