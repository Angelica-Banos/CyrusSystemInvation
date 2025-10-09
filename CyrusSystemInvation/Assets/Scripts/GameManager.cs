using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // --- Instancia única (Singleton) ---
    public static GameManager Instance { get; private set; }
    public static Arbol arbol;
    public Vertice EscenaActual;
    public int contraseñacounter = 0;
    public GameObject piso;

    public void contraseñas() { 
        contraseñacounter += 1;
        if(contraseñacounter >= 4) { 
            piso.SetActive(false);
            contraseñacounter = 0;
            EscenaActual.completado = true;
        }
    }
   

// --- Variables globales ---

    void Awake()
    {
        // Si ya existe un GameManager y no es este, destrúyelo
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        // Asignar la instancia y marcar para no destruirla
        Instance = this;
        DontDestroyOnLoad(gameObject);

        // Buscar automáticamente el árbol si no está asignado
        if (arbol == null)
        {
            arbol = FindFirstObjectByType<Arbol>();
            if (arbol == null)
            {
                Debug.LogWarning("⚠ No se encontró el árbol en la escena actual. Si esta es la escena de título, es normal.");
            }
            else
            {
                Debug.Log("🌳 Árbol encontrado automáticamente.");
            }
        }

        // Escuchar los cambios de escena
        SceneManager.sceneLoaded += OnSceneLoaded;

        arbol = GetComponent<Arbol>();

    }
   
    public void ActualizarNodoIz()
    {
        EscenaActual = EscenaActual.izquierdo;

        // Buscar automáticamente el árbol si no está asignado
        if (arbol == null)
        {
            arbol = FindFirstObjectByType<Arbol>();
            if (arbol == null)
            {
                Debug.LogWarning("No se encontró el árbol en la escena actual. Si esta es la escena de título, es normal.");
            }
            else
            {
                Debug.Log("Árbol encontrado automáticamente.");
            }
        }

        //Escuchar los cambios de escena
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    public void ActualizarBase() { 
        EscenaActual = arbol.raiz;
    }
    

    // --- Se ejecuta cuando se carga una nueva escena ---
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        piso = GameObject.FindGameObjectWithTag("Piso");

        if (piso == null)
        {
            Debug.LogWarning("No se encontr� ning�n objeto con el tag 'Piso'.");
        }
        else
        {
            Debug.Log("Piso encontrado: " + piso.name);
        }
        if(EscenaActual.completado == true) { 
            piso.SetActive(false);
        }
        else { 
            piso.SetActive(true);
        }
        // Si el árbol no está asignado, lo buscamos de nuevo
        if (arbol == null)
        {
            arbol = FindFirstObjectByType<Arbol>();
            if (arbol != null)
                Debug.Log($"🌱 Árbol asignado en la escena: {scene.name}");
        }

        // Buscar mapa visual en la nueva escena
        MapaVisual mapa = FindFirstObjectByType<MapaVisual>();
        if (mapa != null && arbol != null)
        {
            Debug.Log($"🗺 MapaVisual encontrado en la escena {scene.name}, configurando...");

            // Configurar el mapa con el árbol actual
            mapa.Configurar(arbol);

            // Mostrar el estado actual del árbol (nodos visibles según la escena)
            mapa.ActualizarMapa();

            Debug.Log("✅ MapaVisual sincronizado correctamente con el árbol y el nodo actual.");
        }
    }

    // --- Actualizar referencias de posición del jugador en el árbol ---
    
    public void ActualizarNodoDe()
    {
        if (EscenaActual != null && EscenaActual.derecho != null)
        {
            EscenaActual = EscenaActual.derecho;
            Debug.Log($"➡ Movido al nodo derecho: {EscenaActual.nombreEscena}");
        }
        else
        {
            Debug.LogWarning("⚠ No hay nodo derecho disponible desde la escena actual.");
        }
    }

    // --- Carga una escena específica del nodo actual ---
    public void CargarEscenaActual()
    {
        if (EscenaActual == null)
        {
            Debug.LogWarning("⚠ No se puede cargar la escena actual: el nodo actual es nulo.");
            return;
        }

        Debug.Log($"🎬 Cargando escena: {EscenaActual.nombreEscena}");
        SceneManager.LoadScene(EscenaActual.nombreEscena);
    }

    // --- Limpiar eventos al destruir ---
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
