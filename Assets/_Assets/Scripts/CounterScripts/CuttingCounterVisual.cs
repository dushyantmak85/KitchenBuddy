using System;
using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.EventSystems;

public class CuttingCounterVisual : MonoBehaviour
{
    [SerializeField] private CuttingCounter cuttingCounter;

    private const string animationTrigger = "Cut";

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        cuttingCounter.OnCut += CuttingCounter_OnInteractionWithCounter;

    }

    
    private void CuttingCounter_OnInteractionWithCounter(object sender, EventArgs e)
    {

        animator.SetTrigger(animationTrigger);
    }

}

