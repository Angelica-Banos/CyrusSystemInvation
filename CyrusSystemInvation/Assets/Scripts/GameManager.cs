using UnityEngine;

public class GameManager : MonoBehaviour
{
    // --- Instancia única (Singleton) ---
    public static GameManager Instance { get; private set; }
    public static Arbol arbol;

    // --- Variables globales ---
    public Vertice EscenaActual;



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
