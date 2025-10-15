using UnityEngine;
using UnityEngine.SceneManagement;

public class ControladorMusica : MonoBehaviour
{
    public static ControladorMusica instancia;

    [Header("Clips de música")]
    public AudioClip audiowin;
    public AudioClip audionodos;
    public AudioClip audioplataformer;

    private AudioSource audioSource;

    private void Awake()
    {
        if (instancia == null)
        {
            instancia = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        // Añadir un AudioSource si no existe
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        audioSource.loop = true;
        audioSource.playOnAwake = false;

        // Escuchar el evento de cambio de escena
        SceneManager.activeSceneChanged += OnSceneChanged;
    }

    private void Start()
    {
        // Reproducir la música adecuada al inicio
        CambiarMusica(SceneManager.GetActiveScene().buildIndex);
    }

    private void OnSceneChanged(Scene anterior, Scene nueva)
    {
        CambiarMusica(nueva.buildIndex);
    }

    private void CambiarMusica(int indexEscena)
    {
        if (indexEscena == 10)
        {
            audioSource.Pause();
            return;
        }
        AudioClip clipElegido;
       
        if (indexEscena == 5)
            clipElegido = audioplataformer; // escena número 6
        else if (indexEscena == 6) {
            clipElegido = audiowin; // escena número 7
        }
        else
            clipElegido = audionodos; // todas las demás escenas

        // Si ya está sonando el mismo clip, no lo reinicies
        if (audioSource.clip == clipElegido && audioSource.isPlaying)
            return;

        audioSource.clip = clipElegido;
        audioSource.Play();
        
    }
}
