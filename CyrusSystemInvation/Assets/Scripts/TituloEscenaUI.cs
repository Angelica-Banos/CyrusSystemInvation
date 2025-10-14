using System.Collections;
using UnityEngine;
using TMPro;

public class TituloEscenaUI : MonoBehaviour
{
    [Header("Configuración del título principal")]
    public TextMeshProUGUI textoTitulo;
    public CanvasGroup grupoCanvas;

    [Header("Mensajes adicionales")]
    public TextMeshProUGUI[] mensajes; // Se llenan dinámicamente

    [Header("Tiempos de animación")]
    public float duracionVisible = 2f;
    public float duracionDesvanecer = 1f;

    void Start()
    {
        // Asigna el contenido dinámico desde el GameManager (si existe)
        if (GameManager.Instance != null)
        {
            string titulo = GameManager.Instance.ObtenerTituloDeEscena();
            string[] textos = GameManager.Instance.ObtenerMensajesDeEscena();

            if (textoTitulo != null)
                textoTitulo.text = titulo;

            for (int i = 0; i < mensajes.Length; i++)
            {
                if (i < textos.Length)
                {
                    mensajes[i].text = textos[i];
                    mensajes[i].gameObject.SetActive(true);
                }
                else
                {
                    mensajes[i].gameObject.SetActive(false);
                }
            }
        }

        // Inicia invisible
        grupoCanvas.alpha = 0f;
        StartCoroutine(MostrarYDesvanecer());
    }

    private IEnumerator MostrarYDesvanecer()
    {
        yield return StartCoroutine(FadeCanvas(0f, 1f, duracionDesvanecer));
        yield return new WaitForSeconds(duracionVisible);
        yield return StartCoroutine(FadeCanvas(1f, 0f, duracionDesvanecer));
        Destroy(gameObject);
    }

    private IEnumerator FadeCanvas(float desde, float hasta, float duracion)
    {
        float tiempo = 0f;
        while (tiempo < duracion)
        {
            tiempo += Time.deltaTime;
            float t = tiempo / duracion;
            grupoCanvas.alpha = Mathf.Lerp(desde, hasta, t);
            yield return null;
        }
        grupoCanvas.alpha = hasta;
    }
}
