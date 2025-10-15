using UnityEngine;
using UnityEngine.SceneManagement;

public class EscenesMove : MonoBehaviour
{

    public void Jugar() {
        SceneManager.LoadScene(10);

    }
    public void Creditos() {
        SceneManager.LoadScene(9);
    }

    public void Titulo()
    {
        SceneManager.LoadScene(0);
    }
    public void salir() {
        Application.Quit();
    }
}
