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

        if (WebCamTexture.devices.Length > 0)
        {
            camaraWeb = new WebCamTexture();
            vistaCamara.texture = camaraWeb;
            camaraWeb.Play();
        }
        else
        {
            Debug.LogWarning("No se detectó cámara.");
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

        // 🔹 Cada 5 segundos tomar una foto
        contadorFoto += Time.deltaTime;
        if (contadorFoto >= intervaloFoto)
        {
            contadorFoto = 0f;
            TomarFoto();   // 🔸 Este es el que llama el método de la cámara
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

        // Ajuste de tamaño al Canvas
        nuevoPez.transform.localScale = new Vector3(0.4f, 0.4f, 1f);

        // Posición aleatoria
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

    // 🔸 Método original que mantiene la resolución y aplica pixelado real
    void TomarFoto()
    {
        if (camaraWeb == null || !camaraWeb.isPlaying) return;

        // Captura de la textura original
        Texture2D foto = new Texture2D(camaraWeb.width, camaraWeb.height);
        foto.SetPixels(camaraWeb.GetPixels());
        foto.Apply();

        // 🔸 Aplica efecto pixelado reduciendo resolución y reescalando
        int factorPixel = 20; // cuanto más alto, más pixelado (recomiendo entre 10 y 30)
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

        // Escalar hacia arriba de nuevo (manteniendo el efecto pixelado)
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

        // Crear sprite desde la imagen pixelada
        Sprite spriteFoto = Sprite.Create(final, new Rect(0, 0, final.width, final.height), new Vector2(0.5f, 0.5f));

        // Instanciar como pez (foto del jugador)
        GameObject nuevoPez = Instantiate(prefabImagen, contenedorPeces);
        nuevoPez.GetComponent<SpriteRenderer>().sprite = spriteFoto;
        nuevoPez.transform.localScale = new Vector3(0.25f, 0.25f, 1f);
        float yPos = Random.Range(-2.5f, 2.5f);
        nuevoPez.transform.localPosition = new Vector3(-12f, yPos, 0f);

        // 🔹 Marcar que este pez es una foto del jugador
        Pez scriptPez = nuevoPez.GetComponent<Pez>();
        if (scriptPez == null) scriptPez = nuevoPez.AddComponent<Pez>();
        scriptPez.esFotoJugador = true;

        pecesActivos.Add(nuevoPez);
        StartCoroutine(MoverPez(nuevoPez));
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

        // Mostrar mensaje solo si era una foto tomada por la cámara
        if (esFotoJugador)
            MostrarMensaje("¡Bien hecho! Capturaste información sensible.");
    }

    void ActualizarPuntaje()
    {
        if (textoPuntaje != null)
            textoPuntaje.text = "Puntaje: " + puntajeActual;
    }

    void MostrarMensaje(string mensaje)
    {
        panelMensaje.SetActive(true);
        textoMensaje.text = mensaje;
        Time.timeScale = 0f;
    }

    public void CerrarMensaje()
    {
        panelMensaje.SetActive(false);
        Time.timeScale = 1f;
    }

    void FinalizarJuego()
    {
        juegoActivo = false;
        camaraWeb.Stop();

        if (puntajeActual >= objetivoMinimo)
            SceneManager.LoadScene("Nodo_02"); // si alcanzó el objetivo
        else
            SceneManager.LoadScene(SceneManager.GetActiveScene().name); // si no, reinicia el minijuego
    }
}