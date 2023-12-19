using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private Button playeButton;
    [SerializeField] private Button quitButton;

    private void Awake()
    {
        playeButton.onClick.AddListener(() =>
        {
            Loader.LoaderScene(Loader.Scene.MainScene);
            Time.timeScale = 1f;
        });
        quitButton.onClick.AddListener(() =>
        {
            Application.Quit();
        });

    }
}
