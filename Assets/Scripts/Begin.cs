using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class VBegin : MonoBehaviour
{
    public VideoPlayer videoPlayer;

    void Start()
    {
        if (videoPlayer)
        {
            videoPlayer.loopPointReached += OnVideoFinished;
            videoPlayer.Play();
        }
        else
        {
            SceneManager.LoadScene("Main");
        } 
    }

    void OnVideoFinished(VideoPlayer vp)
    {
        SceneManager.LoadScene("Main");
    }
}
