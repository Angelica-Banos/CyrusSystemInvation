using UnityEngine;
using UnityEngine.SceneManagement;

public class EscenesMove : MonoBehaviour
{

    public void Jugar() {
        SceneManager.LoadScene(1);

    }
    public void Creditos() {
        SceneManager.LoadScene(9);
    }
    public void salir() {
        Application.Quit();
    }
}
