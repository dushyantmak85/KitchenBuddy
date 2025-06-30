using System.Net.NetworkInformation;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class TImerUI : MonoBehaviour
{
    [SerializeField] private Image TimerImage;

    private void Start()
    {
        gameObject.SetActive(false);
        GameManager.Instance.OnGameStateChanged += GameManager_OnGameStateChanged;
    }

    private void GameManager_OnGameStateChanged(object sender,System.EventArgs e)
    {
        if (GameManager.Instance.IsGamePlaying())
        {
            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        TimerImage.fillAmount= GameManager.Instance.ReturnTimer();

        if(GameManager.Instance.ReturnTimer() > 0.6)
        {
           TimerImage.color = Color.red;
        }
       
    }


}
