using UnityEngine;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private TMPro.TextMeshProUGUI RecipesCountText;
    [SerializeField] private AudioSource GameOverSound;

    private void Start()
    {
        GameManager.Instance.OnGameStateChanged += GameManager_OnGameStateChanged;
        gameObject.SetActive(false); // Initially hide the gameOver UI
    }

    private void GameManager_OnGameStateChanged(object sender, System.EventArgs e)
    {
        if (GameManager.Instance.IsGameOver())
        {
            GameOverSound.Play();

            gameObject.SetActive(true);

            RecipesCountText.text = DeliveryManager.Instance.NumberOfRecipesDelivered.ToString();   
        }
      
    }

   
}
