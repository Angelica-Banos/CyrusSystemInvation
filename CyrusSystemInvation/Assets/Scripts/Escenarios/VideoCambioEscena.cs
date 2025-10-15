using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class VideoCambioEscena : MonoBehaviour
{
    public VideoPlayer videoPlayer;

    void Start()
    {
        videoPlayer.loopPointReached += OnVideoFinished;
    }

    //El metodo ollama el evento del video
    void OnVideoFinished(VideoPlayer vp)
    {
        SceneManager.LoadScene(1);
    }

    void OnDisable()
    {
        // Clean up the subscription when the object is disabled or destroyed
        videoPlayer.loopPointReached -= OnVideoFinished;
    }

    public void SaltarEscena()
    {
        videoPlayer.Stop();
        SceneManager.LoadScene(1);
    }
}
