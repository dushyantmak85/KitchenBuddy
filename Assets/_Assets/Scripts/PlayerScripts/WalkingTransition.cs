using UnityEngine;

public class WalkingTransition : MonoBehaviour
{
    private Animator animator;
    private const string IsWalking = "IsWalking";
    [SerializeField] private PlayerController player;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {

        animator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool(IsWalking, player.Iswalking());
        
    }
}
