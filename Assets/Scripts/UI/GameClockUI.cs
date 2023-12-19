using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameClockUI : MonoBehaviour
{
    [SerializeField] private Image clock;
    private void Awake()
    {
        clock.fillAmount = 0;
    }

    private void Update()
    {
        clock.fillAmount = KitchenGameManager.Instance.GetGamePlayingTimerNomorlized();     
    }
}
