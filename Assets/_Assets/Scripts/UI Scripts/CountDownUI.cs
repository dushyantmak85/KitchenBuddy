using UnityEngine;

public class CountDownUI : MonoBehaviour
{
    [SerializeField] private TMPro.TextMeshProUGUI countDownText;

    private void Start()
    {
        GameManager.Instance.OnGameStateChanged += GameManager_OnGameStateChanged;
        gameObject.SetActive(false); // Initially hide the countdown UI
    }

    private void GameManager_OnGameStateChanged(object sender, System.EventArgs e)
    {
        if (GameManager.Instance.IsCountDownToStart())
        {
           
            gameObject.SetActive(true); 
        }
        else
        {
            gameObject.SetActive(false); // Hide the countdown UI when not in countdown state
        }
    }

    private void Update()
    {
        countDownText.text= Mathf.Ceil(GameManager.Instance.ReturnCountDownTimer()).ToString();
       
    }
}
