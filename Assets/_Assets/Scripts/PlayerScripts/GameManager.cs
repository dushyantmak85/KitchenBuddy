using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Playables;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance { get; private set; }
    [SerializeField] private GameObject SoundManager;
  
    public event System.EventHandler OnGameStateChanged;
    private enum GameState {
        WaitingToStart,
        CountDownToStart,
        Playing,
        GameOver
    }

    private float waitingTimer = 1f;

    private  float   CountDownTimer = 3f;

    private float GameOverTimerMax = 50f;
    private float GameOverTimer;

    private GameState currentGameState;
    private void Awake()
    {
        currentGameState = GameState.WaitingToStart;
        Instance = this;
    }

    private void Update()
    {
        switch (currentGameState) {
            case GameState.WaitingToStart:
                waitingTimer -= Time.deltaTime;
                if (waitingTimer <= 0f)
                {
                    currentGameState = GameState.CountDownToStart;
                    OnGameStateChanged?.Invoke(this, System.EventArgs.Empty); // Notify subscribers that the game state has changed
                }
                break;
            case GameState.CountDownToStart:
                CountDownTimer -= Time.deltaTime;
                if (CountDownTimer <= 0f)
                {
                    currentGameState = GameState.Playing;
                    OnGameStateChanged?.Invoke(this, System.EventArgs.Empty);
                    GameOverTimer = GameOverTimerMax; // Reset the game over timer to its maximum value
                }
                break;

            case GameState.Playing:
                GameOverTimer -= Time.deltaTime;
                if (GameOverTimer <= 0f)
                {
                    currentGameState = GameState.GameOver;
                    OnGameStateChanged?.Invoke(this, System.EventArgs.Empty); // Notify subscribers that the game state has changed

                }
                break;
            case GameState.GameOver:
                SoundManager.GetComponent<AudioSource>().Stop();               
                break;

        }
       

    }

    public bool IsGamePlaying()
    {
        return currentGameState == GameState.Playing;
    }

    public bool IsCountDownToStart()
    {
        return currentGameState == GameState.CountDownToStart;
    }

    public float ReturnCountDownTimer()
    {
        return CountDownTimer;
    }

    public bool IsGameOver()
    {
        return currentGameState == GameState.GameOver;
    }

    public float ReturnTimer()
    {
        return 1- (GameOverTimer / GameOverTimerMax); // Returns a value between 0 and 1 representing the progress of the game over timer
    }

}
