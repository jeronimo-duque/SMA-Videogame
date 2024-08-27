using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private string sceneToLoad;

    public void LoadScene(string sceneName)
    {
        Debug.Log($"Go to {sceneName}");
        SceneManager.LoadScene(sceneName);
    }
}
