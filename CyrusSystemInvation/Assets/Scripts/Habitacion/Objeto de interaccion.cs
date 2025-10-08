using UnityEngine;

public class Objetodeinteraccion : MonoBehaviour
{
    public int id;
    public void Interaccion() { 
        if (id == 0) {
            Debug.Log("Has interactuado con el objeto 0");
        }
        else if (id == 1) {
            Debug.Log("Has interactuado con el objeto 1");
        }
    }
}
