using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using UnityEngine.UI;

public class ControladorMapa : MonoBehaviour
{
    public List<GameObject> vertices = new List<GameObject>();
    public List<GameObject> nodoseguro;
    public GameObject mapa;
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
        buscado(busc);

    }
    public void buscado(int bus) {
        
        int bus1 = bus - 30;
        nodoseguro[bus1].SetActive(true);
        nodoseguro[bus1].GetComponent<Image>().color = Color.green;
        
    }
    public void ColorearNodoActual()
    {
        vertices[gameManager.EscenaActual.identificador].GetComponent<Image>().color = Color.red;

    }
    private void Update()
    {
        if(gameManager.MinijuegoActivo)
        {
            mapa.SetActive(false);
        }
        else
        {
            mapa.SetActive(true);
            this.ColorearNodoActual();
        }
    }
    public void ActualizarMapa()
    {
        ActualizarMapaRecursivo(GameManager.arbol.raiz);
    }
    public void ActualizarMapaRecursivo(Vertice a)
    {
        if (a == null) return;
        else 
        {
            if (a.esNodoBuscado)
            {
                busc = a.identificador;
                return;
            }
            nodosExistentes.Add(a.identificador);
            vertices[a.identificador].SetActive(true);
            ActualizarMapaRecursivo(a.izquierdo);
            ActualizarMapaRecursivo(a.derecho);
        }
    }
}
