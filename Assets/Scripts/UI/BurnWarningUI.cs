using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurnWarningUI : MonoBehaviour
{
    [SerializeField] StoveCounter stoveCounter;

    private void Start()
    {
        stoveCounter.OnBarUIChanged += StoveCounter_OnBarUIChanged;
        Hide();
    }

    private void StoveCounter_OnBarUIChanged(object sender, IHasProgress.OnBarUIChangedEventArgs e)
    {
        float burningTimer = 0.5f;
        bool playWarning = stoveCounter.IsFried() && e.fillNomarlized >= burningTimer; 
        if (playWarning)
        {
            Show();
        }
        else
        {
            Hide();
        }
    }
    private void Show()
    {
        gameObject.SetActive(true);
    }
    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
