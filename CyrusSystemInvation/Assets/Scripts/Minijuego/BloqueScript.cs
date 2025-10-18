using UnityEngine;
using UnityEngine.SceneManagement;


public class BloqueScript : Objetodeinteraccion
{
    public int numero;
    private Renderer rend;
    void Start()
    {
        gameManager = GameManager.Instance;
        rend = GetComponent<Renderer>();
        if (numero == null) { 
            numero = Random.Range(0, 9);
        }
    }
    public override void Interaccion()
    {
        if (SceneManager.GetActiveScene().buildIndex == 11) {
            if (numero == 0)
            {
                rend.material.color = Color.green;
                
            }
            else
            {
                rend.material.color = Color.red;
            }

        }
        else { 
        if (numero == 0)
        {
            rend.material.color = Color.green;
            gameManager.contraseñas();
        }
        else
        {
            rend.material.color = Color.red;
        } 
    }
    }
}
