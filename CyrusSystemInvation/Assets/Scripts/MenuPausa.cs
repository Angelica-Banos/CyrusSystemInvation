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
        Time.timeScale = 1f;
        isPaused = false;
    }
    public void Pause()
    {
        menuPausaUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }
    public void salir()
    {
        SceneManager.LoadScene(0);
    }
}
