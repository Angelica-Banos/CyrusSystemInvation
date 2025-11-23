using UnityEngine;

public class Puerta : MonoBehaviour
{
    [Header("Configuración")]
    public string playerTag = "Player";
    public bool useTrigger = true;              // usa OnTriggerEnter si true
    public float aperturaDistancia = 2f;        // fallback por distancia si useTrigger == false

    [Header("Animator")]
    public string animatorTriggerParameter = "Open"; // nombre del Trigger en el Animator
    public bool buscarAnimatorEnHijos = true;

    private Animator animator;
    private Transform player;
    private bool abierto = false;

    void Awake()
    {
        // Obtener Animator en este objeto o en hijos
        animator = GetComponent<Animator>();
        if (animator == null && buscarAnimatorEnHijos)
            animator = GetComponentInChildren<Animator>();

        var go = GameObject.FindGameObjectWithTag(playerTag);
        if (go != null) player = go.transform;

        // --- Asegurar que hay un collider marcado como trigger ---
        bool tieneTrigger = false;
        foreach (var col in GetComponents<Collider>())
        {
            if (col != null && col.isTrigger)
            {
                tieneTrigger = true;
                break;
            }
        }

        if (!tieneTrigger && useTrigger)
        {
            // Intentar convertir MeshCollider existente en trigger si es seguro
            var meshCol = GetComponent<MeshCollider>();
            if (meshCol != null)
            {
                meshCol.isTrigger = true;
                // Si el MeshCollider es no-convex y el jugador tiene Rigidbody dinámico, puede fallar.
                Debug.Log($"Puerta '{name}': convertí MeshCollider a trigger. Si no funciona, añade un BoxCollider (Is Trigger) y marca Convex si es necesario.");
            }
            else
            {
                // Añadir BoxCollider como fallback y marcarlo trigger
                var r = GetComponentInChildren<Renderer>();
                var bc = gameObject.AddComponent<BoxCollider>();
                bc.isTrigger = true;
                if (r != null)
                {
                    // ajustar tamaño y centro aproximado al renderer
                    Vector3 size = r.bounds.size;
                    Vector3 centerWorld = r.bounds.center;
                    bc.center = transform.InverseTransformPoint(centerWorld);
                    bc.size = size;
                }
                Debug.Log($"Puerta '{name}': no había Collider trigger — añadí BoxCollider (Is Trigger) en tiempo de ejecución.");
            }
        }
    }

    void Start()
    {
        if (animator == null)
            Debug.LogWarning($"Puerta '{name}': no se encontró Animator. Añádelo al GameObject o en un hijo.");
        else
            Debug.Log($"Puerta '{name}': Animator = {(animator.runtimeAnimatorController != null ? animator.runtimeAnimatorController.name : "null")}.");

        // Comprobar jugador y Rigidbody para ayudar al debug
        if (player == null)
            Debug.LogWarning($"Puerta '{name}': no encontré GameObject con tag '{playerTag}'. Asegúrate del tag.");
        else
        {
            var rb = player.GetComponent<Rigidbody>();
            if (rb == null)
                Debug.LogWarning($"Puerta '{name}': el jugador '{player.name}' no tiene Rigidbody. Sin Rigidbody pueden no dispararse triggers correctamente.");
        }
    }

    void Update()
    {
        if (abierto) return;

        if (!useTrigger)
        {
            if (player == null)
            {
                var go = GameObject.FindGameObjectWithTag(playerTag);
                if (go != null) player = go.transform;
            }
            if (player != null)
            {
                float d = Vector3.Distance(player.position, transform.position);
                if (d <= aperturaDistancia)
                    AbrirPuerta();
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (!useTrigger || abierto) return;
        if (other.CompareTag(playerTag))
        {
            Debug.Log($"Puerta '{name}': OnTriggerEnter detectado por '{other.name}'.");
            AbrirPuerta();
        }
    }

    // Método público para abrir por código o testing
    public void AbrirPuerta()
    {
        if (abierto) return;
        abierto = true;

        if (animator == null)
        {
            Debug.LogWarning($"Puerta '{name}': Animator no asignado, no se puede reproducir animación.");
            return;
        }

        // Preferir trigger del Animator (recomendado)
        if (!string.IsNullOrEmpty(animatorTriggerParameter))
        {
            bool tieneParam = false;
            foreach (var p in animator.parameters)
            {
                if (p.name == animatorTriggerParameter)
                {
                    tieneParam = true;
                    break;
                }
            }

            if (tieneParam)
            {
                Debug.Log($"Puerta '{name}': activando trigger Animator '{animatorTriggerParameter}'.");
                animator.SetTrigger(animatorTriggerParameter);
                return;
            }
            else
            {
                Debug.LogWarning($"Puerta '{name}': el parámetro '{animatorTriggerParameter}' no existe en el Animator. Comprueba el nombre en la ventana Animator.");
            }
        }

        Debug.LogWarning($"Puerta '{name}': no se pudo usar trigger; añade una transición por Trigger o usa el método AbrirPuertaEditor para testear.");
    }

    [ContextMenu("Test Abrir Puerta")]
    private void AbrirPuertaEditor()
    {
        AbrirPuerta();
    }
}
