using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TutorialUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI moveUpText;
    [SerializeField] private TextMeshProUGUI moveDownText;
    [SerializeField] private TextMeshProUGUI moveRightText;
    [SerializeField] private TextMeshProUGUI moveLeftText;
    [SerializeField] private TextMeshProUGUI interactText;
    [SerializeField] private TextMeshProUGUI interacAlternateText;
    [SerializeField] private TextMeshProUGUI pauseText;
    private void Start()
    {
        Show();
        UpdateVisual();
        KitchenGameManager.Instance.OnResumeGame += KitchenGameManager_OnResumeGame;
        GameInput.Instance.OnRibiBindingAction += GameInput_OnRibiBindingAction;
    }

    private void GameInput_OnRibiBindingAction(object sender, System.EventArgs e)
    {
        UpdateVisual();
    }

    private void KitchenGameManager_OnResumeGame(object sender, System.EventArgs e)
    {
        Hide();
    }

    private void UpdateVisual()
    {
       
        moveUpText.text = GameInput.Instance.GetBindingText(GameInput.Binding.MoveUp);
        moveDownText.text = GameInput.Instance.GetBindingText(GameInput.Binding.MoveDown);
        moveRightText.text = GameInput.Instance.GetBindingText(GameInput.Binding.MoveRight);
        moveLeftText.text = GameInput.Instance.GetBindingText(GameInput.Binding.MoveLeft);
        interactText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Interact);
        interacAlternateText.text = GameInput.Instance.GetBindingText(GameInput.Binding.InteractAlternate);
        pauseText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Pause);
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
