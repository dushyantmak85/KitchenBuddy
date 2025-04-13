using System;
using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.EventSystems;

public class ContainerCounterVisual : MonoBehaviour
{
    [SerializeField] private ContainerCounter containerCounter;

    private const string animationTrigger = "OpenClose";

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        containerCounter.OnInteractionWithContainer += ContainerCounter_OnInteractionWithContainer;

    }

    
    private void ContainerCounter_OnInteractionWithContainer(object sender, EventArgs e)
    {

        animator.SetTrigger(animationTrigger);
    }

}

