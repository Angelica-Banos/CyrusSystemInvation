using UnityEngine;
using UnityEngine.SceneManagement;

public class EscenesMove : MonoBehaviour
{

    void Jugar() {
        SceneManager.LoadScene(1);

    }
    void Tutorial() { 
        
    }
    void Creditos() { 
        
    }
    void salir() {
        Application.Quit();
    }
}
