using UnityEngine;
using UnityEngine.SceneManagement;

public class Objetodeinteraccion : MonoBehaviour
{
    public int id;
    public GameManager gameManager;
    void Start() {
        gameManager = GameManager.Instance;
    }
    public virtual void Interaccion() { 
        if (id == 0) {
            Debug.Log("Has interactuado con el objeto Derecha");
            if (gameManager.EscenaActual.derecho == null) {
                // aviso
                Debug.Log("no hay nada a la derecha");
            }
            else { 
                GameManager.Instance.ActualizarNodoDe();
                UnityEngine.SceneManagement.SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
            }
        }
        else if (id == 1) {
            Debug.Log("Has interactuado con el objeto Izquierda");
            if (gameManager.EscenaActual.izquierdo == null)
            {
                // aviso
                Debug.Log("no hay nada a la izquierda");

            }
            else
            {
                GameManager.Instance.ActualizarNodoIz();
                UnityEngine.SceneManagement.SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
        }
        else if (id == 2) {
            Debug.Log("Has interactuado con el objeto de la Base");
           GameManager.Instance.ActualizarBase();
            UnityEngine.SceneManagement.SceneManager.LoadScene(1);

        }
    }
}
