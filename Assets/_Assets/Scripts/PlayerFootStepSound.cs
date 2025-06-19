using UnityEngine;

public class PlayerFootStepSound : MonoBehaviour
{
    private PlayerController player;
    private float stepMaxTimer=.1f;
    private float stepTimer;

    private void Awake()
    {
        player=GetComponent<PlayerController>();
    }

    private void Update()
    {
        if (player.IsWalking)
        {
            stepTimer -= Time.deltaTime;
            if (stepTimer <= 0f)
            {
                stepTimer = stepMaxTimer;
                float volume = 200f;
                SoundManagerScript.instance.PlayFootStepSound(player.transform.position,volume);
            }
        }
        else
        {
            stepTimer = 0f;
        }
    }

}
