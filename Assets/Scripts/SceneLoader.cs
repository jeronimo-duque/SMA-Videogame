using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class SceneLoader : MonoBehaviour
{
    public VideoPlayer videoPlayer;  
    public string sceneToLoad;       

    public void PlayVideoAndLoadScene()
    {
        
        videoPlayer.Play();

        
        videoPlayer.loopPointReached += OnVideoFinished;
    }

    private void OnVideoFinished(VideoPlayer vp)
    {
        
        SceneManager.LoadScene(sceneToLoad);
    }
}
