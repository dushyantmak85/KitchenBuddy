using UnityEngine;

public class StoveCounterVisual : MonoBehaviour
{
    [SerializeField] GameObject SizzlingParticles;
    [SerializeField] GameObject StoveOnVisual;
    [SerializeField] StoveCounterScript Counter;

    private void Start()
    {
        Counter.OnStateChanged += Counter_OnStateChanged;

    }


    private void Counter_OnStateChanged(object Sender, StoveCounterScript.OnStateChangedEventArgs e)
    {
        bool state = e.state == StoveCounterScript.State.Cooking || e.state == StoveCounterScript.State.Cooked;
        
            SizzlingParticles.SetActive(state);
            StoveOnVisual.SetActive(state);
        


    }
}
