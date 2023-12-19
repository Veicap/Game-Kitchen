using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashBarUI : MonoBehaviour
{
    private const string IS_FLASH = "IsFlash";
    private Animator animator;
    [SerializeField] StoveCounter stoveCounter;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    private void Start()
    {
        stoveCounter.OnBarUIChanged += StoveCounter_OnBarUIChanged;
        animator.SetBool(IS_FLASH, false);
    }

    private void StoveCounter_OnBarUIChanged(object sender, IHasProgress.OnBarUIChangedEventArgs e)
    {
        float burningTimer = 0.5f;
        bool playWarning = stoveCounter.IsFried() && e.fillNomarlized >= burningTimer;
        animator.SetBool(IS_FLASH, playWarning);
    }
   
}
