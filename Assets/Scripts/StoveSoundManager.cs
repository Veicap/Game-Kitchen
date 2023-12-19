using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveSoundManager : MonoBehaviour
{
    [SerializeField] private StoveCounter stoveCounter;
    private AudioSource audioSource;
    private float burnWarningTimer;
    private bool playWarning;
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    private void Start()
    {
        stoveCounter.OnStateChanged += StoveCounter_OnStateChanged;
        stoveCounter.OnBarUIChanged += StoveCounter_OnBarUIChanged;
    }

    private void StoveCounter_OnBarUIChanged(object sender, IHasProgress.OnBarUIChangedEventArgs e)
    {
        float burningTimer = 0.5f;
        playWarning = stoveCounter.IsFried() && e.fillNomarlized >= burningTimer;
    }

    private void StoveCounter_OnStateChanged(object sender, StoveCounter.OnStateChangedEventArgs e)
    {
        bool playSound = e.state == StoveCounter.State.Frying || e.state == StoveCounter.State.Fried;
        if (playSound)
        {
            audioSource.Play();
        }
        else
        {
            audioSource.Pause();
        }
    }
    private void Update()
    {
        if(playWarning)
        {
            burnWarningTimer -= Time.deltaTime;
            if (burnWarningTimer < 0f)
            {
                float burnWarningTimerMax = 0.2f;
                burnWarningTimer = burnWarningTimerMax;
                SoundManager.Instance.PlayWarningSound(stoveCounter.transform.position);
            }
        }
        
    }
}
