using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenGameManager : MonoBehaviour
{
    public static KitchenGameManager Instance { get; private set; }
    public event EventHandler OnStateChanged;
    public event EventHandler OnPauseGame;
    public event EventHandler OnResumeGame;
    
    private enum State
    {
        WaitingToStart,
        CountDownToStart,
        GamePlaying,
        GameOver,

    }
    private State state;
    private float countDownToStartTimer = 3f;
    private float gamePlayingTimer;
    private float gamePlayingTimerMax = 105f;
    
    private void Awake()
    {
        Instance = this;
        state = State.WaitingToStart;
        OnStateChanged?.Invoke(this, EventArgs.Empty);
    }
    private void Start()
    {
        GameInput.Instance.OnToggleGamePause += GameInput_OnPauseGame;
        GameInput.Instance.OnInteractAction += GameInput_OnInteractAction;
    }

    private void GameInput_OnInteractAction(object sender, EventArgs e)
    {
        if(state == State.WaitingToStart) {
            state = State.CountDownToStart;
            OnStateChanged?.Invoke(this, EventArgs.Empty);
            OnResumeGame?.Invoke(this, EventArgs.Empty);
        }
    }

    private void GameInput_OnPauseGame(object sender, EventArgs e)
    {
        PauseGame();
    }

    private void Update()
    {
        switch(state)
        {
            case State.WaitingToStart:
                break;
            case State.CountDownToStart:
                countDownToStartTimer -= Time.deltaTime;
                if(countDownToStartTimer < 0f)
                {
                    gamePlayingTimer = gamePlayingTimerMax;
                    state = State.GamePlaying;
                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                }
                break;
            case State.GamePlaying:
                gamePlayingTimer -= Time.deltaTime;
                if(gamePlayingTimer < 0f)
                {
                    state = State.GameOver;
                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                }
                break;
            case State.GameOver:
                OnStateChanged?.Invoke(this, EventArgs.Empty);
                break;
        }
        
    }
    public float GetCountDownTimer()
    {
        return countDownToStartTimer;
    }
    public bool IsCountDownState()
    {
        return state == State.CountDownToStart;
    }
    public bool IsGamePlaying()
    {
        return state == State.GamePlaying;
    }
    public bool IsGameOver()
    {
        return state == State.GameOver;
    }
    public float GetGamePlayingTimerNomorlized()
    {
        return 1 - (gamePlayingTimer / gamePlayingTimerMax);
    }
    public void PauseGame()
    {
        
        Time.timeScale = 0f;
        OnPauseGame?.Invoke(this, EventArgs.Empty);
    }
}
