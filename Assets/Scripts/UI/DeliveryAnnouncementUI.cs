using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeliveryAnnouncementUI : MonoBehaviour
{
    private const string POPUP = "PopUp";
    [SerializeField] private Image backGroundSuccess;
    [SerializeField] private Image backGroundFailure;
    [SerializeField] private TextMeshProUGUI deliverySucceessText;
    [SerializeField] private TextMeshProUGUI deliveryFailureText;
    [SerializeField] private Color colorSuccess;
    [SerializeField] private Color colorFailure;
    private Animator animator;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        Hide();
        DeliveryManager.Instance.OnDeliverSuccess += DeliveryManager_OnDeliverSuccess;
        DeliveryManager.Instance.OnDeliverFailure += DeliveryManager_OnDeliverFailure;
    }

    private void DeliveryManager_OnDeliverFailure(object sender, System.EventArgs e)
    {
        
        Show();
        animator.SetTrigger(POPUP);
        backGroundFailure.color = colorFailure;
        deliveryFailureText.text = "DELIVERY\nFAILED";

    }

    private void DeliveryManager_OnDeliverSuccess(object sender, System.EventArgs e)
    {
        
        Show();
        animator.SetTrigger(POPUP);
        backGroundSuccess.color = colorSuccess;
        deliverySucceessText.text = "DELIVERY\nSUCCESSED";
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
