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
            }
        }
        
        SceneManager.UnloadSceneAsync("Minijuego1");

        Time.timeScale = 1f;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        GameObject.Find("BotonesAndroid").GetComponent<ControlesAndroid>().mostrar();
    }
}
