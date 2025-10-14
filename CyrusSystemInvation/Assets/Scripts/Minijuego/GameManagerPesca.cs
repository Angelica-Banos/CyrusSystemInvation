using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManagerPesca : MonoBehaviour
{
    [Header("Configuración del juego")]
    public float tiempoJuego = 30f;
    public int objetivoMinimo = 10;
    private int puntajeActual = 0;
    private float tiempoRestante;
    private bool juegoActivo = false;
    public CanvasGroup grupoCanvas;

    [Header("Cámara del jugador")]
    public RawImage vistaCamara;
    private WebCamTexture camaraWeb;

    [Header("Peces e imágenes")]
    public GameObject prefabImagen; // Prefab del pez
    public Transform contenedorPeces;
    public Sprite[] imagenes; // Sprites de los peces normales
    private List<GameObject> pecesActivos = new List<GameObject>();

    [Header("Caña del jugador")]
    public GameObject caña;
    public float velocidadCaña = 5f;

    [Header("UI")]
    public TMP_Text textoTiempo;
    public TMP_Text textoPuntaje;
    public GameObject panelMensaje;
    public TMP_Text textoMensaje;

    private float intervaloFoto = 5f;
    private float contadorFoto = 0f;

    void Start()
    {
        tiempoRestante = tiempoJuego;
        juegoActivo = true;
        panelMensaje.SetActive(false);

        try
        {
            if (WebCamTexture.devices.Length > 0)
            {
                camaraWeb = new WebCamTexture();
                vistaCamara.texture = camaraWeb;
                camaraWeb.Play();
                Debug.Log("📷 Cámara iniciada correctamente.");
            }
            else
            {
                Debug.LogWarning("⚠️ No se detectó cámara en el dispositivo.");
            }
        }
        catch (System.Exception e)
        {
            Debug.LogError("❌ Error al intentar iniciar la cámara: " + e.Message);
        }

        StartCoroutine(GenerarPeces());
        ActualizarPuntaje();
    }

    void Update()
    {
        if (!juegoActivo) return;

        // Movimiento de la caña
        float moverY = Input.GetAxis("Vertical") * velocidadCaña * Time.deltaTime;
        float moverX = Input.GetAxis("Horizontal") * velocidadCaña * Time.deltaTime;
        caña.transform.Translate(moverX, moverY, 0);

        // Temporizador general del juego
        tiempoRestante -= Time.deltaTime;
        textoTiempo.text = "Tiempo: " + Mathf.CeilToInt(tiempoRestante) + "s";

        // Cada 5 segundos tomar una foto
        contadorFoto += Time.deltaTime;
        if (contadorFoto >= intervaloFoto)
        {
            contadorFoto = 0f;
            TomarFoto();
        }

        // Cuando se acaba el tiempo
        if (tiempoRestante <= 0)
        {
            FinalizarJuego();
        }
    }

    IEnumerator GenerarPeces()
    {
        while (juegoActivo)
        {
            Sprite imagenSeleccionada = imagenes[Random.Range(0, imagenes.Length)];
            GameObject nuevoPez = CrearPez(imagenSeleccionada, false);
            yield return new WaitForSeconds(2.5f); // peces más espaciados
        }
    }

    GameObject CrearPez(Sprite sprite, bool esFotoJugador)
    {
        GameObject nuevoPez = Instantiate(prefabImagen, contenedorPeces);
        SpriteRenderer sr = nuevoPez.GetComponent<SpriteRenderer>();
        sr.sprite = sprite;

        nuevoPez.transform.localScale = new Vector3(0.4f, 0.4f, 1f);
        float y = Random.Range(-2.5f, 2.5f);
        nuevoPez.transform.localPosition = new Vector3(-12f, y, 0f);

        Pez pezScript = nuevoPez.AddComponent<Pez>();
        pezScript.esFotoJugador = esFotoJugador;
        pecesActivos.Add(nuevoPez);

        StartCoroutine(MoverPez(nuevoPez));
        return nuevoPez;
    }

    IEnumerator MoverPez(GameObject pez)
    {
        while (pez != null && pez.transform.localPosition.x < 12f)
        {
            pez.transform.Translate(Vector3.right * 2f * Time.deltaTime);
            yield return null;
        }

        if (pez != null)
            Destroy(pez);
    }

    // 📸 Método protegido con try-catch
    void TomarFoto()
    {
        try
        {
            if (camaraWeb == null || !camaraWeb.isPlaying)
            {
                Debug.LogWarning("⚠️ No se puede tomar foto: cámara no disponible o detenida.");
                return;
            }

            Texture2D foto = new Texture2D(camaraWeb.width, camaraWeb.height);
            foto.SetPixels(camaraWeb.GetPixels());
            foto.Apply();

            // Aplica efecto pixelado
            int factorPixel = 20;
            Texture2D pixelada = new Texture2D(foto.width / factorPixel, foto.height / factorPixel);

            for (int y = 0; y < pixelada.height; y++)
            {
                for (int x = 0; x < pixelada.width; x++)
                {
                    Color colorPromedio = foto.GetPixelBilinear((float)x / pixelada.width, (float)y / pixelada.height);
                    pixelada.SetPixel(x, y, colorPromedio);
                }
            }
            pixelada.Apply();

            Texture2D final = new Texture2D(foto.width, foto.height);
            for (int y = 0; y < final.height; y++)
            {
                for (int x = 0; x < final.width; x++)
                {
                    Color colorPixel = pixelada.GetPixelBilinear((float)x / final.width, (float)y / final.height);
                    final.SetPixel(x, y, colorPixel);
                }
            }
            final.Apply();

            Sprite spriteFoto = Sprite.Create(final, new Rect(0, 0, final.width, final.height), new Vector2(0.5f, 0.5f));

            GameObject nuevoPez = Instantiate(prefabImagen, contenedorPeces);
            nuevoPez.GetComponent<SpriteRenderer>().sprite = spriteFoto;
            nuevoPez.transform.localScale = new Vector3(0.25f, 0.25f, 1f);
            float yPos = Random.Range(-2.5f, 2.5f);
            nuevoPez.transform.localPosition = new Vector3(-12f, yPos, 0f);

            Pez scriptPez = nuevoPez.GetComponent<Pez>();
            if (scriptPez == null) scriptPez = nuevoPez.AddComponent<Pez>();
            scriptPez.esFotoJugador = true;

            pecesActivos.Add(nuevoPez);
            StartCoroutine(MoverPez(nuevoPez));
        }
        catch (System.Exception e)
        {
            Debug.LogError("❌ Error al intentar tomar foto o acceder a la cámara: " + e.Message);
        }
    }

    public void Pescar(GameObject pez, bool esFotoJugador)
    {
        if (pez != null)
        {
            Destroy(pez);
            pecesActivos.Remove(pez);
        }

        puntajeActual++;
        ActualizarPuntaje();

        if (esFotoJugador)
            StartCoroutine(MostrarMensaje());

    }

    void ActualizarPuntaje()
    {
        if (textoPuntaje != null)
            textoPuntaje.text = "Puntaje: " + puntajeActual;
    }

    public IEnumerator MostrarMensaje()
    {
        Time.timeScale = 0f;
        panelMensaje.SetActive(true);
        yield return new WaitForSecondsRealtime(1f);
        panelMensaje.SetActive(false);
        Time.timeScale = 1f;
    }



    public void CerrarMensaje()
    {
        panelMensaje.SetActive(false);
        Time.timeScale = 1f;
    }

    void FinalizarJuego()
    {
        try
        {
            if (camaraWeb != null && camaraWeb.isPlaying)
                camaraWeb.Stop();
        }
        catch (System.Exception e)
        {
            Debug.LogWarning("⚠️ Error al detener la cámara: " + e.Message);
        }

        juegoActivo = false;

        if (puntajeActual >= objetivoMinimo)
        {
            GameManager.Instance.EscenaActual.completado = true;
            GameManager.Instance.minijuego2win = true;
            SceneManager.LoadScene(3);
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
