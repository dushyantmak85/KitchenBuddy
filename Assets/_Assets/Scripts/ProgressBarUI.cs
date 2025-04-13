using UnityEngine;
using UnityEngine.UI;

public class ProgressBarUI : MonoBehaviour
{
    [SerializeField] private Image BarImage;
    [SerializeField] private CuttingCounter cuttingCounter;

    public void Start()
    {
        cuttingCounter.OnCuttingProgressChanged += CuttingCounter_OnCuttingProgressChanged;
        BarImage.fillAmount = 0f;
    }

    private void CuttingCounter_OnCuttingProgressChanged(object sender, CuttingCounter.OnCuttingProgressChangedEventArgs e)
    {
        BarImage.fillAmount = e.progressNormalized;
    }
}
