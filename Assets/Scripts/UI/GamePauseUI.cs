using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GamePauseUI : MonoBehaviour
{
    public static GamePauseUI Instance { get; private set; }
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button mainMenuButton;
    [SerializeField] private Button optionButton;
    private bool isAllowedShowGamePauseUI = true;
    private void Awake()
    {
        Instance = this;
        resumeButton.onClick.AddListener(() =>
        {
            Time.timeScale = 1f;
            isAllowedShowGamePauseUI = true;
            Hide();
        });
        mainMenuButton.onClick.AddListener(() =>
        {
            Loader.LoaderScene(Loader.Scene.MainMenuScene);
        });
        optionButton.onClick.AddListener(() =>
        {
            GameOptionUI.Instance.Show();
            
            Hide();
            GameOptionUI.Instance.OnSoundEffectButtonSelect();
        });
    }
    private void Start()
    {
        KitchenGameManager.Instance.OnPauseGame += KitchenGameManager_OnPauseGame;
        Hide();
    }

    

    private void KitchenGameManager_OnPauseGame(object sender, System.EventArgs e)
    {
        
        if(isAllowedShowGamePauseUI)
        {
            isAllowedShowGamePauseUI = false;
            Show();
            OnResumeSelectButton();
        }
        
    }

    

    public void Show()
    {
        gameObject.SetActive(true);
    }
    private void Hide()
    {
        gameObject.SetActive(false);
    }
    public void OnResumeSelectButton()
    {
        resumeButton.Select();
    }
}
