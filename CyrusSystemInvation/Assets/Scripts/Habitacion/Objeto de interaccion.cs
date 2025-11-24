using System;
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
        try
        {
            if (gameManager.EscenaActual.izquierdo == null && gameManager.EscenaActual.derecho == null)
            {
                Debug.Log("no hay nada en ningun nodo lol");
                if (id == 2)
                {
                    Debug.Log("Has interactuado con el objeto de la Base");
                    GameManager.Instance.ActualizarBase();
                    UnityEngine.SceneManagement.SceneManager.LoadScene(1);

                }
                return;
            }

            if (JuegoGanado())
            {
                Debug.Log("Has ganado el juego");
                GameManager.Instance.GanasteElJuego();
                if (id == 2)
            {
                Debug.Log("Has interactuado con el objeto de la Base");
                GameManager.Instance.ActualizarBase();
                UnityEngine.SceneManagement.SceneManager.LoadScene(1);

            }
                return;
            }

            if (id == 0)
            {
                Debug.Log("Has interactuado con el objeto Derecha");
                if (gameManager.EscenaActual.derecho == null)
                {
                    // aviso
                    Debug.Log("no hay nada a la derecha");
                }
                else
                {
                    Debug.Log(gameManager.EscenaActual.derecho.identificador);
                    GameManager.Instance.ActualizarNodoDe();
                    UnityEngine.SceneManagement.SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                }
            }
            else if (id == 1)
            {
                Debug.Log("Has interactuado con el objeto Izquierda");
                if (gameManager.EscenaActual.izquierdo == null)
                {
                    // aviso
                    Debug.Log("no hay nada a la izquierda");

                }
                else
                {
                    Debug.Log(gameManager.EscenaActual.izquierdo.identificador);
                    GameManager.Instance.ActualizarNodoIz();
                    UnityEngine.SceneManagement.SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                }
            }
            else if (id == 2)
            {
                Debug.Log("Has interactuado con el objeto de la Base");
                GameManager.Instance.ActualizarBase();
                UnityEngine.SceneManagement.SceneManager.LoadScene(1);

            }
            else if(id == 4) {
                UnityEngine.SceneManagement.SceneManager.LoadScene(0);

            }
            else
            {
                Debug.Log("Otra instancia de objeto interactuable");
            }
        }
        catch(Exception e)
        {
            Debug.LogError(e.Message);
        }
       
    }

    private Boolean JuegoGanado()
    {
        //Si el derecho existe y es el nodo buscado, o si el izquierdo existe y es el nodo buscado, entonces ganaste
        if (gameManager.EscenaActual.derecho != null && gameManager.EscenaActual.derecho.esNodoBuscado)
        {
            return true;
        }else if( gameManager.EscenaActual.izquierdo != null && gameManager.EscenaActual.izquierdo.esNodoBuscado)
        {
            return true;
        }
        return false;
    }
}
