using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // --- Instancia única (Singleton) ---
    public static GameManager Instance { get; private set; }
    public static Arbol arbol;

    // --- Variables globales ---
    public Vertice EscenaActual;
    public int contrase�acounter = 0;
    public GameObject piso;

    public void contrase�as() { 
        contrase�acounter += 1;
        if(contrase�acounter >= 4) { 
            piso.SetActive(false);
        }
    }
    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
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
    }

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

        arbol = GetComponent<Arbol>();
    }
    public void ActualizarBase() { 
        EscenaActual = arbol.raiz;
    }
    public void ActualizarNodoIz() { 
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

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (arbol == null)
        {
            arbol = FindFirstObjectByType<Arbol>();
            if (arbol != null)
            {
                Debug.Log($"Árbol asignado en la escena: {scene.name}");
            }
        }
    }

    public void ActualizarBase()
    {
        if (arbol == null)
            arbol = FindFirstObjectByType<Arbol>();

        if (arbol != null)
            EscenaActual = arbol.raiz;
    }

    public void ActualizarNodoIz()
    {
        if (EscenaActual != null)
            EscenaActual = EscenaActual.izquierdo;
    }

    public void ActualizarNodoDe()
    {
        if (EscenaActual != null)
            EscenaActual = EscenaActual.derecho;
    }
}
