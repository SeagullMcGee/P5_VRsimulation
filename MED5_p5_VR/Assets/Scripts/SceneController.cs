using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    [Tooltip("The build index of the scene to load. Set this in the Inspector.")]
    public int sceneBuildIndex; // Scene index to load, configurable in the Inspector

    public void WhatSceneToLoad()
    {
        // Load the scene using the specified build index
        SceneManager.LoadScene(sceneBuildIndex);
    }

    public void QuitSim()
    {
        Application.Quit();
    }
}
