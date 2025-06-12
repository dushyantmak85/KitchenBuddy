using UnityEngine;

public class StoveCounterSound : MonoBehaviour
{
    private AudioSource audioSource;
    [SerializeField] StoveCounterScript stoveCounter;

    private void Awake()
    {
        audioSource= GetComponent<AudioSource>();       
    }

    private void Start()
    {
        stoveCounter.OnStateChanged += PlaySound_OnStateChanged;
    }

    private void PlaySound_OnStateChanged(object sender, StoveCounterScript.OnStateChangedEventArgs e)
    {
        bool playSound = e.state == StoveCounterScript.State.Cooking || e.state == StoveCounterScript.State.Cooked;
        if (playSound)
        {
            audioSource.Play();
        }
        else
        {
            audioSource.Pause();
        }
    }
}
