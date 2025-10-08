using UnityEngine;
using UnityEngine.SceneManagement;

public class AbrirMinijuego : MonoBehaviour
{
    private bool minijuegoCargado = false;

    void OnMouseDown()
    {
        if (!minijuegoCargado)
        {
            SceneManager.LoadScene("Minijuego1", LoadSceneMode.Additive);
            Camera.main.enabled = false;
            minijuegoCargado = true;

            // Opcional: Pausar el juego 3D
            Time.timeScale = 0f;

            // Mostrar el cursor si lo tenías bloqueado
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}
