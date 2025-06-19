using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance { get; private set; }

    public event System.EventHandler OnGameStateChanged;
    private enum GameState {
        WaitingToStart,
        CountDownToStart,
        Playing,
        GameOver
    }

    private float waitingTimer = 1f;

    private  float   CountDownTimer = 3f;

    private float GameOverTimer = 10f;

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

                }
                break;

            case GameState.Playing:
                GameOverTimer -= Time.deltaTime;
                if (GameOverTimer <= 0f)
                {
                    currentGameState = GameState.GameOver;
                    
                }
                break;
            case GameState.GameOver:
                break;

        }
        Debug.Log(currentGameState);    

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
    

}
