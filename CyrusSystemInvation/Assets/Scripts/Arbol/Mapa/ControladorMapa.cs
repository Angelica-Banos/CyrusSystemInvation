using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using UnityEngine.UI;
public class ControladorMapa : MonoBehaviour { 
    public List<GameObject> vertices = new List<GameObject>();
    public List<GameObject> nodoseguro; public GameObject mapa; 
    public GameManager gameManager; 
    public int busc; 
    public List<int> nodosExistentes = new List<int>();
    private void Start()
    {
        gameManager = GameManager.Instance;
        if (gameManager == null)
        {
            Debug.LogError("No se encontró una instancia de GameManager en la escena.");
            return;
        }
        ActualizarMapa();
        Debug.LogWarning("El nodo actual es: " + vertices[gameManager.EscenaActual.identificador]);
        vertices[gameManager.EscenaActual.identificador].GetComponent<Image>().color = Color.red;
        
        
        
    }


    private void Update() {
        if (gameManager.MinijuegoActivo) {
            mapa.SetActive(false);
        } else { 
            mapa.SetActive(true);
        } 
    }

    public int ObtenerDireccionNodoSeguro(Vertice actual, Vertice seguro)
    {
        if (actual == null || seguro == null) return -1;

        // Si el nodo actual ES el nodo seguro
        if (actual == seguro) return 0;

        // --------------- NIVEL 1: hijos directos ---------------
        if (actual.izquierdo == seguro) return 1;
        if (actual.derecho == seguro) return 2;

        // --------------- NIVEL 2: nietos del nodo actual ---------------

        // IZQUIERDA
        if (actual.izquierdo != null)
        {
            if (actual.izquierdo.izquierdo == seguro) return 3; // izq-izq
            if (actual.izquierdo.derecho == seguro) return 4; // izq-der

            // Buscar más profundo
            if (EstaEnSubarbol(actual.izquierdo, seguro))
            {
                // Si es más profundo, la dirección es la del hijo
                return 1; // viene por el hijo izquierdo
            }
        }

        // DERECHA
        if (actual.derecho != null)
        {
            if (actual.derecho.izquierdo == seguro) return 5; // der-izq
            if (actual.derecho.derecho == seguro) return 6; // der-der

            // Buscar más profundo
            if (EstaEnSubarbol(actual.derecho, seguro))
            {
                return 2; // viene por el hijo derecho
            }
        }

        // Si no pertenece al subárbol del nodo actual
        return -1;
    }
    private bool EstaEnSubarbol(Vertice raiz, Vertice objetivo)
    {
        if (raiz == null) return false;
        if (raiz == objetivo) return true;

        return EstaEnSubarbol(raiz.izquierdo, objetivo) ||
               EstaEnSubarbol(raiz.derecho, objetivo);
    }

    public void ActualizarMapa()
    {
        // Apagar todo primero
        for (int i = 0; i < vertices.Count; i++)
            vertices[i].SetActive(false);

        Vertice actual = gameManager.EscenaActual;

        // SIEMPRE encendemos el nodo actual
        vertices[0].SetActive(true);
        vertices[0].GetComponent<Image>().color = Color.red;

        // --------------------------
        // NIVEL 1 — HIJOS DIRECTOS
        // --------------------------

        // IZQUIERDO
        if (actual.izquierdo != null)
        {
            vertices[1].SetActive(true);
        }

        // DERECHO
        if (actual.derecho != null)
        {
            vertices[2].SetActive(true);
        }

        // --------------------------
        // NIVEL 2 — NIETOS DIRECTOS
        // --------------------------

        // NIETOS DEL HIJO IZQUIERDO
        if (actual.izquierdo != null)
        {
            if (actual.izquierdo.izquierdo != null)
                vertices[3].SetActive(true);

            if (actual.izquierdo.derecho != null)
                vertices[4].SetActive(true);
        }

        // NIETOS DEL HIJO DERECHO
        if (actual.derecho != null)
        {
            if (actual.derecho.izquierdo != null)
                vertices[5].SetActive(true);

            if (actual.derecho.derecho != null)
                vertices[6].SetActive(true);
        }

        // --------------------------
        // ILUMINAR NODO SEGURO
        // --------------------------
        int direccion = ObtenerDireccionNodoSeguro(actual, gameManager.arbolaux.nodobuscado);

        if (direccion >= 1 && direccion <= 6)
        {
            vertices[direccion].GetComponent<Image>().color = Color.green;
            vertices[direccion].SetActive(true);
        }
    }


}