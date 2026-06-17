using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class VideoLoadScene : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public string SampleScene;

    void Start()
    {
        videoPlayer.loopPointReached += EndReached;
        videoPlayer.Play();
    }

    void EndReached(VideoPlayer vp)
    {
        SceneManager.LoadScene(SampleScene);
    }
}