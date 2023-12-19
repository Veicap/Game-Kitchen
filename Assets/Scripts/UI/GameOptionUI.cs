using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOptionUI : MonoBehaviour
{
    public static GameOptionUI Instance { get; private set; }
    [SerializeField] private Button soundEffectButton;
    [SerializeField] private Button musicButton;
    [SerializeField] private Button closeButton;
    [SerializeField] private TextMeshProUGUI soundEffectText;
    [SerializeField] private TextMeshProUGUI musicEffectText;
    [SerializeField] private Button moveUpButton;
    [SerializeField] private Button moveDownButton;
    [SerializeField] private Button moveLeftButton;
    [SerializeField] private Button moveRightButton;
    [SerializeField] private Button interactButton;
    [SerializeField] private Button interactAlternateButton;
    [SerializeField] private Button pauseButton;
    [SerializeField] private TextMeshProUGUI keyMoveUpText;
    [SerializeField] private TextMeshProUGUI keyMoveDownText;
    [SerializeField] private TextMeshProUGUI keyMoveLeftText;
    [SerializeField] private TextMeshProUGUI keyMoveRightText;
    [SerializeField] private TextMeshProUGUI keyInteractText;
    [SerializeField] private TextMeshProUGUI keyInteractAlternateText;
    [SerializeField] private TextMeshProUGUI keyPauseText;
    [SerializeField] private Transform pressKeyUI;

    private void Awake()
    {
        Instance = this;
        soundEffectButton.onClick.AddListener(() =>
        {
            // have fuction change sound effect right here
            SoundManager.Instance.ChangeSoundEffect();
            UpdateVisual();
        });
        musicButton.onClick.AddListener(() =>
        {
            // have function change muscic right here
            MusicManager.Instance.ChangeMusic();
            UpdateVisual(); 
        });
        closeButton.onClick.AddListener(() =>
        {
            //Hide visual
            Hide();
            GamePauseUI.Instance.Show();
            GamePauseUI.Instance.OnResumeSelectButton();
        });
        moveUpButton.onClick.AddListener(() => {RebiBinding(GameInput.Binding.MoveUp);});
        moveDownButton.onClick.AddListener(() => { RebiBinding(GameInput.Binding.MoveDown); });
        moveLeftButton.onClick.AddListener(() => { RebiBinding(GameInput.Binding.MoveLeft); });
        moveRightButton.onClick.AddListener(() => { RebiBinding(GameInput.Binding.MoveRight); });
        interactButton.onClick.AddListener(() => { RebiBinding(GameInput.Binding.Interact); });
        interactAlternateButton.onClick.AddListener(() => { RebiBinding(GameInput.Binding.InteractAlternate); });
        pauseButton.onClick.AddListener(() => { RebiBinding(GameInput.Binding.Pause); });
    }
    private void Start()
    {
        UpdateVisual();
        Hide();
        HidePressKeyUI();
    }
    private void UpdateVisual()
    {
        soundEffectText.text = "Sound Effect: " + Mathf.Round(SoundManager.Instance.GetVolume() * 10);
        musicEffectText.text = "Music: " + Mathf.Round(MusicManager.Instance.GetVolume() * 10);
        keyMoveUpText.text = GameInput.Instance.GetBindingText(GameInput.Binding.MoveUp);
        keyMoveDownText.text = GameInput.Instance.GetBindingText(GameInput.Binding.MoveDown);
        keyMoveLeftText.text = GameInput.Instance.GetBindingText(GameInput.Binding.MoveLeft);
        keyMoveRightText.text = GameInput.Instance.GetBindingText(GameInput.Binding.MoveRight);
        keyInteractText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Interact);
        keyInteractAlternateText.text = GameInput.Instance.GetBindingText(GameInput.Binding.InteractAlternate);
        keyPauseText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Pause);
    }
    public void Show()
    {
        gameObject.SetActive(true);
    }
    public void Hide()
    {
        gameObject.SetActive(false);
    }
    private void RebiBinding(GameInput.Binding nameOfBinding)
    {
        ShowPressKeyUI();
        GameInput.Instance.RebiBinding(nameOfBinding, () =>
        {
            HidePressKeyUI();
            UpdateVisual();
        });
        
    }
    private void ShowPressKeyUI()
    {
        pressKeyUI.gameObject.SetActive(true);
    }
    private void HidePressKeyUI()
    {
        pressKeyUI.gameObject.SetActive(false);
    }
    public void OnSoundEffectButtonSelect()
    {
        soundEffectButton.Select();
    }
}
