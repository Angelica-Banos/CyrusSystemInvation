using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPausa : MonoBehaviour
{
    public bool isPaused = false;
    public GameObject menuPausaUI;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
        
    }
    public void Resume()
    {
        menuPausaUI.SetActive(false);
        isPaused = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Time.timeScale = 1f;

        GameObject.Find("BotonesAndroid").GetComponent<ControlesAndroid>().mostrar();
    }
    public void Pause()
    {
        menuPausaUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        GameObject.Find("BotonesAndroid").GetComponent<ControlesAndroid>().ocultar();
    }
    public void salir()
    {
        Time.timeScale = 1f;
        Destroy(GameObject.Find("GameManager"));
        Debug.LogError("GameManager Eliminado");
        SceneManager.LoadScene(0);
    }
}
