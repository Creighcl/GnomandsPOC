using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    public void LoadSceneById(int sceneId)
    {
        SceneManager.LoadScene(sceneId);
    }

    public void LoadChosenScene(Scenes scene)
    {
        SceneManager.LoadScene((int)scene);
    }
}
