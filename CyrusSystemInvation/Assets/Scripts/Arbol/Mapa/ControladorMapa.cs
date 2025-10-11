using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using UnityEngine.UI;

public class ControladorMapa : MonoBehaviour
{
    public List<GameObject> vertices = new List<GameObject>();
    public GameObject mapa;
    public GameManager gameManager;
    public int busc;
    public List<int> nodosExistentes = new List<int>();
    private void Start()
    {
        gameManager = GameManager.Instance;
        ActualizarMapa();
        Debug.LogWarning("El nodo es: " + vertices[gameManager.EscenaActual.identificador]);
        vertices[gameManager.EscenaActual.identificador].GetComponent<Image>().color = Color.red;
        vertices[busc].GetComponent<Image >().color = Color.green;

    }
    public void ColorearNodoActual()
    {
        vertices[gameManager.EscenaActual.identificador].GetComponent<Image>().color = Color.red;
        vertices[busc].GetComponent<Image>().color = Color.green;
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
        else if (!nodosExistentes.Contains(a.identificador))
        {
            if (a.esNodoBuscado)
            {
                busc = a.identificador;
            }
            nodosExistentes.Add(a.identificador);
            vertices[a.identificador].SetActive(true);
            ActualizarMapaRecursivo(a.izquierdo);
            ActualizarMapaRecursivo(a.derecho);
        }
    }
}
