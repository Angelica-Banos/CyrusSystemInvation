using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class TituloEscenaUI : MonoBehaviour
{
    [Header("Configuración del título")]
    public TextMeshProUGUI textoTitulo;
    public CanvasGroup grupoCanvas;
    public float duracionVisible = 5f;
    public float duracionDesvanecer = 1.5f;

    void Start()
    {
        grupoCanvas.alpha = 0;

        // Obtiene el nombre de la escena actual
        string nombreEscena = SceneManager.GetActiveScene().name;

        // Intenta obtener el nombre bonito desde el GameManager
        if (GameManager.Instance != null)
        {
            string nombreBonito = GameManager.Instance.ObtenerNombreBonito(nombreEscena);
            textoTitulo.text = nombreBonito;
        }
        else
        {
            textoTitulo.text = nombreEscena;
        }

        StartCoroutine(MostrarTitulo());
    }

    IEnumerator MostrarTitulo()
    {
        // --- Desvanecer entrada (fade in) ---
        float tiempo = 0;
        while (tiempo < duracionDesvanecer)
        {
            tiempo += Time.deltaTime;
            grupoCanvas.alpha = Mathf.Lerp(0, 1, tiempo / duracionDesvanecer);
            yield return null;
        }

        // --- Mantener visible ---
        yield return new WaitForSeconds(duracionVisible);

        // --- Desvanecer salida (fade out) ---
        tiempo = 0;
        while (tiempo < duracionDesvanecer)
        {
            tiempo += Time.deltaTime;
            grupoCanvas.alpha = Mathf.Lerp(1, 0, tiempo / duracionDesvanecer);
            yield return null;
        }

        Destroy(gameObject);
    }
}
