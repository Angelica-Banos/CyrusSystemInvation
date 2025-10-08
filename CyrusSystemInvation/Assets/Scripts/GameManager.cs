using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // --- Instancia única (Singleton) ---
    public static GameManager Instance { get; private set; }
    public static Arbol arbol;

    // --- Variables globales ---
    public Vertice EscenaActual;
    public int contraseñacounter = 0;
    public GameObject piso;

    public void contraseñas() { 
        contraseñacounter += 1;
        if(contraseñacounter >= 4) { 
            piso.SetActive(false);
        }
    }
    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        piso = GameObject.FindGameObjectWithTag("Piso");

        if (piso == null)
        {
            Debug.LogWarning("No se encontró ningún objeto con el tag 'Piso'.");
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

    }
    public void ActualizarNodoDe() {
        EscenaActual = EscenaActual.derecho;

    }
}
