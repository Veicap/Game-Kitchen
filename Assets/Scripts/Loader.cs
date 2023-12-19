using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class Loader
{
    public enum Scene
    {
        MainMenuScene,
        MainScene,
        LoadingScene,
    }
    private static Scene nameScene; 
    public static void LoaderScene(Scene nameScene)
    {
        Loader.nameScene = nameScene;
        SceneManager.LoadScene(Scene.LoadingScene.ToString());
        
    }
    public static void LoaderCallBack()
    {
        SceneManager.LoadScene(nameScene.ToString());   
    }
}
