using UnityEngine;
using UnityEngine.Video;
using System.Collections;

public class MenuLoader : MonoBehaviour
{
    public GameObject logoLoading;
    public GameObject menuButtons;

    public VideoPlayer videoPlayer;

    void Start()
    {
        menuButtons.SetActive(false);

        StartCoroutine(PrepareVideo());

    }

    IEnumerator PrepareVideo()
    {
        // Começa preparar o vídeo
        videoPlayer.Prepare();

        // Espera terminar de carregar
        while (!videoPlayer.isPrepared)
        {
            yield return null;
            videoPlayer.Play();
        }

        // Espera mais 5 segundos na logo
        yield return new WaitForSeconds(8f);

        // Inicia vídeo


        // Remove logo
        logoLoading.SetActive(false);

        // Mostra menu
        menuButtons.SetActive(true);
    }
}