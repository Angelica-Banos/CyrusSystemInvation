using System.Collections.Generic;
using UnityEngine;

public class ControladorMapa : MonoBehaviour
{
    [Header("Prefab del nodo para mostrar en pantalla")]
    public GameObject nodoPrefab;

    [Header("Referencia al GameManager")]
    public GameManager gameManager;

    [Header("Donde se instanciarán los nodos visibles")]
    public Transform contenedorMapa;

    // Este es el nodo actual donde está el jugador
    public NodoDatos nodoActual;

    // Todos los nodos de pantalla que ya fueron instanciados
    private List<GameObject> nodosInstanciados = new List<GameObject>();

    void Start()
    {
        gameManager = GameManager.Instance;
        nodoActual = gameManager.nodoInicial; // o como obtienes tu nodo inicial

        DibujarNodoActual();
    }

    // -----------------------------
    // DIBUJAR SOLO NODOS REALES
    // -----------------------------
    public void DibujarNodoActual()
    {
        LimpiarMapa();

        // Dibujar nodo actual en el centro
        CrearNodoVisual(nodoActual, Vector2.zero, true);

        // Dibujar hijos reales, no posiciones predefinidas
        if (nodoActual.hijos != null)
        {
            float separacion = 3f;
            float inicioX = -(nodoActual.hijos.Count - 1) * separacion / 2f;

            for (int i = 0; i < nodoActual.hijos.Count; i++)
            {
                NodoDatos hijo = nodoActual.hijos[i];

                Vector2 pos = new Vector2(inicioX + i * separacion, -3f);
                CrearNodoVisual(hijo, pos, false);
            }
        }

        // Dibujar nodo seguro si existe
        NodoDatos seguro = BuscarNodoSeguro(nodoActual);

        if (seguro != null)
        {
            CrearNodoVisual(seguro, new Vector2(6f, 0), false, true);
        }
    }

    // Crear visual de un nodo real
    private void CrearNodoVisual(NodoDatos datos, Vector2 posicion, bool esActual, bool esSeguro = false)
    {
        GameObject go = Instantiate(nodoPrefab, contenedorMapa);
        go.transform.localPosition = posicion;
        nodosInstanciados.Add(go);

        NodoVisual script = go.GetComponent<NodoVisual>();
        script.Configurar(datos.id, esActual, esSeguro);
    }

    // Borra nodos viejos antes de dibujar nuevos
    private void LimpiarMapa()
    {
        foreach (var n in nodosInstanciados)
            Destroy(n);

        nodosInstanciados.Clear();
    }

    // -----------------------------------------
    // BÚSQUEDA DEL NODO SEGURO REAL POR ID
    // -----------------------------------------
    private NodoDatos BuscarNodoSeguro(NodoDatos desde)
    {
        if (desde.nodoSeguroID == -1)
            return null;

        return gameManager.BuscarNodoPorID(desde.nodoSeguroID);
    }

    // Cambio de nodo: cuando el jugador se mueve
    public void MoverANodo(int id)
    {
        nodoActual = gameManager.BuscarNodoPorID(id);
        DibujarNodoActual();
    }
}
