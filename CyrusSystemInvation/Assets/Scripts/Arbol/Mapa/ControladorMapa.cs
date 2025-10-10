using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControladorMapa : MonoBehaviour
{
    public List<GameObject> vertices = new List<GameObject>();
    public GameObject mapa;
    public GameManager gameManager;
    public List<int> nodosExistentes = new List<int>();
    private void Start()
    {
        gameManager = GameManager.Instance;
        ActualizarMapa();
        vertices[gameManager.EscenaActual.identificador].GetComponent<Image >().color = Color.red;
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
        }
    }
    public void ActualizarMapa()
    {
        ActualizarMapaRecursivo(GameManager.arbol.raiz);
    }
    public void ActualizarMapaRecursivo(Vertice a)
    {
        if (a == null) return;
        else if(!nodosExistentes.Contains(a.identificador))
        {
            nodosExistentes.Add(a.identificador);
            vertices[a.identificador].SetActive(true);
            ActualizarMapaRecursivo(a.izquierdo);
            ActualizarMapaRecursivo(a.derecho);
        }
    }
}
