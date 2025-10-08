using UnityEngine;
using UnityEngine.SceneManagement;

public class EscenesMove : MonoBehaviour
{

    public void Jugar() {
        SceneManager.LoadScene(1);

    }
    public void Tutorial() { 
        
    }
    public void Creditos() { 
        
    }
    public void salir() {
        Application.Quit();
    }
}
