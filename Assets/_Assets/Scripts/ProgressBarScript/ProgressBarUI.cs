using UnityEngine;
using UnityEngine.UI;

public class ProgressBarUI : MonoBehaviour
{
    [SerializeField] private Image BarImage;
    [SerializeField] private CuttingCounter cuttingCounter;

    public void Start()
    {
        cuttingCounter.OnCuttingProgressChanged += CuttingCounter_OnCuttingProgressChanged;
        Hide();
        BarImage.fillAmount = 0f;
    }

    private void CuttingCounter_OnCuttingProgressChanged(object sender, CuttingCounter.OnCuttingProgressChangedEventArgs e)
    {
        if (e.progressNormalized<1f)
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
