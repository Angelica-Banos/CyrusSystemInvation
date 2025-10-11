using UnityEngine;
using UnityEngine.SceneManagement;

public class CerrarMinijuego : MonoBehaviour
{
    public void Cerrar()
    {
        GameManager.Instance.MinijuegoActivo = false;
        GameObject playerCameraObject = GameObject.Find("PlayerCamera");
        GameObject Boton = GameObject.Find("Boton");
        Boton.GetComponent<AbrirMinijuego>().minijuegoCargado = false;
        if (playerCameraObject != null)
        {
            Camera playerCam = playerCameraObject.GetComponent<Camera>();
            if (playerCam != null)
            {
                playerCam.enabled = true;
                Debug.Log("PlayerCamera re-enabled.");
            }
            else
            {
                Debug.LogError("PlayerCamera object found, but it has no Camera component attached!");
            }
        }
        else
        {
            Debug.LogError("Could not find GameObject named 'PlayerCamera' in the active scenes. Check the name and scene loading status.");
        }
        SceneManager.UnloadSceneAsync("Minijuego1");

        // Reanudar el juego 3D
        Time.timeScale = 1f;

        // Volver a bloquear el cursor (si tu juego lo usa)
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
