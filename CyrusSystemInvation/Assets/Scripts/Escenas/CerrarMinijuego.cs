using UnityEngine;
using UnityEngine.SceneManagement;

public class CerrarMinijuego : MonoBehaviour
{
    public void Cerrar()
    {
        // Descargar la escena 2D
        Camera.main.enabled = true;
        SceneManager.UnloadSceneAsync("Minijuego2D");

        // Reanudar el juego 3D
        Time.timeScale = 1f;

        // Volver a bloquear el cursor (si tu juego lo usa)
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
