using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class SceneLoader : MonoBehaviour
{
    public VideoPlayer videoPlayer;  // Opcional: asignar un VideoPlayer en el Inspector si se desea reproducir video
    public string sceneToLoad;       // Nombre de la escena a cargar

    // Método que puedes asignar al botón
    public void PlayVideoAndLoadScene()
    {
        if (videoPlayer != null)
        {
            videoPlayer.Play();  // Reproduce el video si está asignado
            videoPlayer.loopPointReached += OnVideoFinished; // Llama a OnVideoFinished cuando el video termina
        }
        else
        {
            // Si no hay VideoPlayer asignado, carga la escena de inmediato
            LoadSceneDirectly();
        }
    }

    // Método que se llama cuando el video finaliza
    private void OnVideoFinished(VideoPlayer vp)
    {
        LoadSceneDirectly();
    }

    // Método para cargar la escena directamente
    private void LoadSceneDirectly()
    {
        UnityEngine.Debug.Log($"Cargando la escena: {sceneToLoad}");
        SceneManager.LoadScene(sceneToLoad);
    }
}
