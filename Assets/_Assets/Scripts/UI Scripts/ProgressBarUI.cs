using System;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarUI : MonoBehaviour
{
    [SerializeField] private GameObject IHasProgress ;
    [SerializeField] private Image BarImage;

    private IHasProgress IhasProgress;
        



    public void Start()
    {
        IhasProgress = IHasProgress.GetComponent<IHasProgress>();
        IhasProgress.OnProgressChanged += HasProgress_OnProgressChanged;

        Hide();
        BarImage.fillAmount = 0f;
    }

    private void HasProgress_OnProgressChanged(object sender, IHasProgress.OnProgressChangedEventArgs e)
    {
        if (e.progressNormalized<1f && e.progressNormalized!=0f)
        {
            Show();
        }
        else
        {
            Hide();

        }
        BarImage.fillAmount = e.progressNormalized;
    }


    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }
}
