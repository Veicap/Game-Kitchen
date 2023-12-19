using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarUICanvas : MonoBehaviour
{
    [SerializeField] private Image barUI;
    [SerializeField] private GameObject gameObjectHasProgress;

    IHasProgress counterHasProgress;
    private void Start()
    {
        counterHasProgress = gameObjectHasProgress.GetComponent<IHasProgress>();
        counterHasProgress.OnBarUIChanged += counterHasProgress_OnBarUIChanged1;
        barUI.fillAmount = 0f;
        Hide();
    }

    private void counterHasProgress_OnBarUIChanged1(object sender, IHasProgress.OnBarUIChangedEventArgs e)
    {
        barUI.fillAmount = e.fillNomarlized;
        if (barUI.fillAmount == 0f || barUI.fillAmount == 1f)
        {
            Hide();
        }
        else
        {
            Show();
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
