using UnityEngine;
using UnityEngine.SceneManagement;

public class EscenesMove : MonoBehaviour
{

    public void JugarIntroduccion() {
      
        GameObject GameManager = new GameObject("GameManager");
        GameManager.AddComponent<Arbol>();
        GameManager.AddComponent<GameManager>();
        SceneManager.LoadScene(10);

    }
    public void Creditos() {
        SceneManager.LoadScene(9);
    }

    public void tutorial() { 
        SceneManager.LoadScene(11);
    }
    public void Titulo()
    {
        
        SceneManager.LoadScene(0);
    }
    public void salir() {
        Application.Quit();
    }
}
