using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // --- Instancia única (Singleton) ---
    public static GameManager Instance { get; private set; }
    public static Arbol arbol;
    public Vertice EscenaActual;
    public bool MinijuegoActivo = false, minijuego2win=false;
    public int contraseñacounter = 0;
    public GameObject piso;
    public Arbol arbolaux;
    [Header("Marcador")]
    public float segundosTranscurridos;
    public int cantidadNodosVisitados;
    private float tiempo = 0f;
    private bool ganador;


    public void contraseñas() { 
        contraseñacounter += 1;
        if(contraseñacounter >= 4) { 
            piso.SetActive(false);
            contraseñacounter = 0;
            EscenaActual.completado = true;
        }
    }

    public void Correo()
    {
            piso.SetActive(false);
            Debug.Log("Correo Completado");
            EscenaActual.completado = true;
    }

    void Update()
    {
        arbolaux = arbol;
        // Solo contar si el juego no está en pausa
        if (!ganador)
        {
            // Sumar tiempo transcurrido desde el último frame
            tiempo += Time.unscaledDeltaTime;

            // Convertir a segundos completos
            if (tiempo >= 1f)
            {
                int segundosAñadir = Mathf.FloorToInt(tiempo);
                segundosTranscurridos += segundosAñadir;
                tiempo -= segundosAñadir; // mantener el residuo de fracción de segundo
            }
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

        tiempo = 0;
        cantidadNodosVisitados = 1; //Empieza en 1 porque ya está en la base
        ganador = false;
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
        EscenaActual = arbol.raiz;  
    }
   
    public void ActualizarNodoIz()
    {

        if (EscenaActual != null && EscenaActual.izquierdo != null)
        {
            EscenaActual = EscenaActual.izquierdo;
            cantidadNodosVisitados = cantidadNodosVisitados + 1;
            Debug.Log($"➡ Movido al nodo izquierdo: {EscenaActual.nombreEscena}");
        }
        else
        {
            Debug.LogWarning("⚠ No hay nodo izquierdo disponible desde la escena actual.");
        }

    }
    public void ActualizarNodoDe()
    {
        if (EscenaActual != null && EscenaActual.derecho != null)
        {
            EscenaActual = EscenaActual.derecho;
            cantidadNodosVisitados = cantidadNodosVisitados + 1;
            Debug.Log($"➡ Movido al nodo derecho: {EscenaActual.nombreEscena}");
        }
        else
        {
            Debug.LogWarning("⚠ No hay nodo derecho disponible desde la escena actual.");
        }
    }

    public void ActualizarBase() { 
        EscenaActual = arbol.raiz;
        cantidadNodosVisitados = cantidadNodosVisitados + 1;
        Debug.Log($"➡ Movido al nodo : {EscenaActual.nombreEscena}");
    }
    

    // --- Se ejecuta cuando se carga una nueva escena ---
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        piso = GameObject.FindGameObjectWithTag("Piso");

        if (piso == null)
        {
            Debug.LogWarning("No se encontró ningún objeto con el tag 'Piso'.");
        }
        else
        {
            Debug.Log("Piso encontrado: " + piso.name);
            if (EscenaActual.completado == true)
            {
                piso.SetActive(false);
            }
            else
            {
                piso.SetActive(true);
            }
        }
       
        // Si el árbol no está asignado, lo buscamos de nuevo
        if (arbol == null)
        {
            arbol = FindFirstObjectByType<Arbol>();
            if (arbol != null)
                Debug.Log($" Árbol asignado en la escena: {scene.name}");
        }

        // Buscar mapa visual en la nueva escena
        MapaVisual mapa = FindFirstObjectByType<MapaVisual>();
        if (mapa != null && arbol != null)
        {
            Debug.Log($" MapaVisual encontrado en la escena {scene.name}, configurando...");

            // Configurar el mapa con el árbol actual
            mapa.Configurar(arbol);

            // Mostrar el estado actual del árbol (nodos visibles según la escena)
            mapa.ActualizarMapa();

            Debug.Log("✅ MapaVisual sincronizado correctamente con el árbol y el nodo actual.");
        }

        GameObject prefabTitulo = Resources.Load<GameObject>("CanvasTituloEscena");
        if (prefabTitulo != null)
        {
            Instantiate(prefabTitulo);
        }
        else
        {
            Debug.LogWarning("No se encontró el prefab 'CanvasTituloEscena' en la carpeta Recursos.");
        }
    }

    
    public void CargarEscenaActual()
    {
        if (EscenaActual == null)
        {
            Debug.LogWarning("No se puede cargar la escena actual: el nodo actual es nulo.");
            return;
        }

        Debug.Log($"Cargando escena: {EscenaActual.nombreEscena}");
        SceneManager.LoadScene(EscenaActual.nombreEscena);
    }

    // --- Limpiar eventos al destruir ---
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void GanasteElJuego() {
        ganador = true;
        SceneManager.LoadScene(6);
        GameObject.Find("BotonesAndroid").GetComponent<ControlesAndroid>().ocultar();
        GameObject.Find("Ganaste").SetActive(true);
    }


    // --- Devuelve un nombre más bonito para el título de cada escena ---
    public string ObtenerNombreBonito(string nombreEscena)
    {
        switch (nombreEscena)
        {
            
            case "Base":
                return "Base Cyrus";
            case "Nodo_01":
                return "Phishing.exe";
            case "Nodo_02":
                return "Salto de directorio";
            case "Nodo_03":
                return "Robo de contraseñas";
            case "Nodo_04":
                return "Infección progresiva";
            case "Nodo_seguro":
                return "Central";
            case "Tutorial":
                return "Tutorial";
            default:
                return " ";
        }
    }

    public string[] ObtenerMensajesDeEscena()
    {
        string nombre = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;

        switch (nombre)
        {
            case "Base":
                return new string[]
                {
                "Bienvenido a la invasion cibernetica.",
                "Preparate para infiltrarte en el sistema.",
                "Tu mision esta por comenzar.",
                "Utiliza WASD para moverte.",
                "Utiliza E para interactuar"
                };

            case "Nodo_03":
                return new string[]
                {
                "Encuentra la combinacion",
                "Utiliza WASD para moverte",
                "Utiliza E para interactuar"
                };

            case "Nodo_01":
                return new string[]
                {
                "Selecciona la opcion correcta",
                "Utiliza CLICK para interactuar"
               
                };

            case "Nodo_02":
                return new string[]
                {
                "Captura todos los archivos que puedas para corromperlos",
                "Utiliza WASD para moverte",
                "Utiliza E para interactuar"
                };
            case "Nodo_04":
                return new string[]
                {
                 "???",
                 "LLega al final sin desaparecer",
                  "Utiliza WASD para moverte",
                  "        Ctrl para agacharte",
                  "        Espacio para saltar",
                  "        E para interactuar"
                };
            case "Nodo_seguro":
                return new string[]
                {
                 
                 "Felicidades [CY.rus]",
                  "¡ Estas a nada de terminar cumplir tu mision !",
                  "Ahora ve al mundo exterior, ESCAPA",
                  "Utiliza WASD para moverte"
                };
            case "Tutorial":
                return new string[]
                {

                 "Bienvenido a la invasion cibernetica.",
                 "Preparate para infiltrarte en el sistema.",
                 "Utiliza WASD para moverte.",
                 "Utiliza E para interactuar.",
                 "Prueba todo lo que puedas y diviertete.",
                };

            default:
                return new string[] { "" };
        }
    }


    public string ObtenerTituloDeEscena()
    {
        // Devuelve el nombre bonito para la escena actual
        string nombreActual = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
        return ObtenerNombreBonito(nombreActual);

    }


}
