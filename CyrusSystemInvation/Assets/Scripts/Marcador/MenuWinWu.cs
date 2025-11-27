using UnityEngine;

public class MenuWinWu : MonoBehaviour
{
    public MenuPausa menuPausa;
    public bool active = true;
    void Update()
    {
        if ( active)
        {
            Time.timeScale = 0f; 
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            Time.timeScale = 1f;
        }
    }
    public void continuar() { 
        active = false;

    }
}
