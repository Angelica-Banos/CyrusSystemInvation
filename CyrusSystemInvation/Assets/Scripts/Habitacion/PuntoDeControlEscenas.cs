using UnityEngine;
using UnityEngine.SceneManagement;

public class PuntoDeControlEscenas : MonoBehaviour
{
    public GameManager gameManager;
    private void Start()
    {
        gameManager = GameManager.Instance;
        gameManager.ActualizarBase();
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
        GameManager.Instance.ActualizarBase();

    }

}
