using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MySceneManager
{

    public static void loadScene(int index)
    {
        SceneManager.LoadScene(index);
    }

    public static void loadScene(string name)
    {
        SceneManager.LoadScene(name);
    }

   public static void loadNextScene()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        if (currentScene < SceneManager.sceneCount )
        {
            SceneManager.LoadScene(currentScene + 1);
        }
    }

    public static void reloadScene()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentScene);
    }

}
