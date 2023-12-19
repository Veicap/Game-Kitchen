using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerCounterVisual : MonoBehaviour
{
    
    [SerializeField] private ContainerCounter containerCounter;
    private const string OPEN_CLOSE = "OpenClose";
    private Animator animator;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    private void Start()
    {
        containerCounter.OnOpenCloseVisual += ContainerCounter_OnOpenCloseVisual;
    }

    private void ContainerCounter_OnOpenCloseVisual(object sender, System.EventArgs e)
    {
        animator.SetTrigger(OPEN_CLOSE);
    }
}
