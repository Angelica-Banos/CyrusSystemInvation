using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;
using System.Collections;

public class VideoIbe : MonoBehaviour
{
    [Header("Opcional: asignar en el Inspector")]
    public VideoPlayer videoPlayer;
    public RawImage imagen;                // RawImage que muestra el vídeo
    public bool destruirAlTerminar = true; // destruir el objeto que contiene este script al terminar
    public int sortingOrderOverlay = 100;  // si es necesario forzar Canvas por encima

    private RenderTexture rt;

    void Awake()
    {
        // Buscar automáticamente en hijos si no se asignó en el inspector
        if (videoPlayer == null)
            videoPlayer = GetComponentInChildren<VideoPlayer>();
        if (imagen == null)
            imagen = GetComponentInChildren<RawImage>();
    }

    IEnumerator Start()
    {
        if (videoPlayer == null)
        {
            Debug.LogWarning("VideoIbe: no se encontró VideoPlayer. Cancelo reproducción.");
            yield break;
        }

        // Si hay AudioSource en el mismo GameObject, úsalo como salida
        if (videoPlayer.audioOutputMode == VideoAudioOutputMode.AudioSource)
        {
            if (videoPlayer.GetTargetAudioSource(0) == null)
            {
                var src = videoPlayer.GetComponent<AudioSource>();
                if (src == null) src = videoPlayer.gameObject.AddComponent<AudioSource>();
                videoPlayer.SetTargetAudioSource(0, src);
            }
        }

        videoPlayer.isLooping = false;
        videoPlayer.playOnAwake = false;

        // Preparar y esperar
        videoPlayer.Prepare();
        while (!videoPlayer.isPrepared)
            yield return null;

        // Si hay RawImage, crear RenderTexture fiable y mostrar
        if (imagen != null)
        {
            if (videoPlayer.targetTexture == null)
            {
                int w = 1920, h = 1080;

                if (videoPlayer.texture != null && videoPlayer.texture.width > 0 && videoPlayer.texture.height > 0)
                {
                    w = Mathf.Clamp((int)videoPlayer.texture.width, 16, 8192);
                    h = Mathf.Clamp((int)videoPlayer.texture.height, 16, 8192);
                }
                else if (videoPlayer.clip != null)
                {
                    w = Mathf.Clamp((int)videoPlayer.clip.width, 16, 8192);
                    h = Mathf.Clamp((int)videoPlayer.clip.height, 16, 8192);
                }

                rt = new RenderTexture(w, h, 0);
                videoPlayer.targetTexture = rt;
            }

            imagen.texture = videoPlayer.targetTexture;

            // Forzar que el RawImage esté activo y visible por encima
            imagen.gameObject.SetActive(true);
            var canvas = imagen.canvas;
            if (canvas != null && canvas.sortingOrder < sortingOrderOverlay)
                canvas.sortingOrder = sortingOrderOverlay;
        }

        // Suscribir evento y reproducir
        videoPlayer.loopPointReached += AlTerminarVideo;
        videoPlayer.Play();
    }

    private void AlTerminarVideo(VideoPlayer vp)
    {
        // desuscribir
        videoPlayer.loopPointReached -= AlTerminarVideo;

        // ocultar solo la imagen (no el Canvas completo)
        if (imagen != null)
        {
            imagen.texture = null;
            imagen.gameObject.SetActive(false);
        }

        // liberar RT si lo creamos
        if (rt != null)
        {
            videoPlayer.targetTexture = null;
            rt.Release();
            Destroy(rt);
            rt = null;
        }

        if (destruirAlTerminar)
            Destroy(gameObject);
    }

    void OnDisable()
    {
        if (videoPlayer != null)
            videoPlayer.loopPointReached -= AlTerminarVideo;
    }
}