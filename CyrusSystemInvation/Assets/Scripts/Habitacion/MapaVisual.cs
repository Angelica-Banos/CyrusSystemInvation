using System;
using System.Collections.Generic;
using UnityEngine;

public class MapaVisual : MonoBehaviour
{
    [Header("Prefabs del Mapa")]
    public GameObject basePrefab;
    public GameObject nodoPrefab;
    public GameObject nodoBuscadoPrefab;
    public GameObject conexionIzqPrefab;
    public GameObject conexionDerPrefab;

    [Header("Configuración del Árbol")]
    public float distanciaHorizontal = 2.5f;
    public float distanciaVertical = 2.0f;
    public float escalaMapa = 0.5f;

    private Dictionary<Vertice, Vector2> posiciones = new Dictionary<Vertice, Vector2>();
    private Dictionary<Vertice, GameObject> nodosInstanciados = new Dictionary<Vertice, GameObject>();
    private Dictionary<(Vertice, Vertice), GameObject> conexiones = new Dictionary<(Vertice, Vertice), GameObject>();
    private Arbol arbolActual;
    private float contadorX = 0f;

    // Nombre del nodo objetivo (asegúrate de usar el mismo en tu árbol)
    private const string NOMBRE_OBJETIVO = "NodoCentralSeguro";

    // -------------------------------
    void Start()
    {
        if (GameManager.arbol != null)
            Configurar(GameManager.arbol);
        else
            Debug.LogWarning("No se encontró el árbol. Asegúrate de que el GameManager tenga referencia a él.");
    }

    // -------------------------------
    public void Configurar(Arbol arbol)
    {
        if (arbol == null) return;
        arbolActual = arbol;

        posiciones.Clear();
        contadorX = 0f;
        AsignarPosicionesInOrder(arbol.raiz, 0);

        // Calcular límites
        float minX = float.MaxValue, maxX = float.MinValue;
        float minY = float.MaxValue, maxY = float.MinValue;
        foreach (var kv in posiciones)
        {
            var p = kv.Value;
            if (p.x < minX) minX = p.x;
            if (p.x > maxX) maxX = p.x;
            if (p.y < minY) minY = p.y;
            if (p.y > maxY) maxY = p.y;
        }

        float ancho = (maxX - minX + 1);
        float alto = (maxY - minY + 1);
        Vector2 offset = new Vector2((ancho - 1) / 2f + minX, (alto - 1) / 2f + minY);

        AjustarEscalaYDistancia(ancho, alto);
        InstanciarVisuales(offset);
        ActualizarMapa();
    }

    // -------------------------------
    void AsignarPosicionesInOrder(Vertice nodo, int nivel)
    {
        if (nodo == null) return;
        AsignarPosicionesInOrder(nodo.izquierdo, nivel + 1);
        posiciones[nodo] = new Vector2(contadorX, nivel);
        contadorX += 1f;
        AsignarPosicionesInOrder(nodo.derecho, nivel + 1);
    }

    // -------------------------------
    void AjustarEscalaYDistancia(float ancho, float alto)
    {
        float factor = Mathf.Max(ancho, alto);

        if (factor <= 3)
        {
            distanciaHorizontal = 3f;
            distanciaVertical = 2.5f;
            escalaMapa = 1.2f;
        }
        else if (factor <= 5)
        {
            distanciaHorizontal = 2.5f;
            distanciaVertical = 2.0f;
            escalaMapa = 1f;
        }
        else if (factor <= 7)
        {
            distanciaHorizontal = 2f;
            distanciaVertical = 1.8f;
            escalaMapa = 0.9f;
        }
        else
        {
            distanciaHorizontal = 1.8f;
            distanciaVertical = 1.5f;
            escalaMapa = 0.8f;
        }

        Debug.Log($"[MapaVisual] Escala ajustada automáticamente (ancho={ancho}, alto={alto})");
    }

    // -------------------------------
    void InstanciarVisuales(Vector2 offset)
    {
        // Limpiar instancias anteriores
        foreach (var go in nodosInstanciados.Values)
            if (go != null) Destroy(go);
        foreach (var go in conexiones.Values)
            if (go != null) Destroy(go);

        nodosInstanciados.Clear();
        conexiones.Clear();

        // Crear los nodos del árbol visual
        foreach (var kv in posiciones)
        {
            Vertice v = kv.Key;
            Vector2 pos = kv.Value;

            GameObject prefab = (v.nombreEscena == "Base") ? basePrefab :
                                 (v.nombreEscena == NOMBRE_OBJETIVO) ? nodoBuscadoPrefab :
                                 nodoPrefab;

            GameObject nodoGO = Instantiate(prefab, transform);
            nodoGO.name = v.nombreEscena;

            // Posición local en el mapa
            float x = (pos.x - offset.x) * distanciaHorizontal * escalaMapa;
            float z = -(pos.y - offset.y) * distanciaVertical * escalaMapa;

            // Base arriba, nodo seguro abajo
            if (v.nombreEscena == "Base")
                z += distanciaVertical * (posiciones.Count / 2f);
            if (v.nombreEscena == NOMBRE_OBJETIVO)
                z -= distanciaVertical * (posiciones.Count / 2f);

            nodoGO.transform.localPosition = new Vector3(x, 0.02f, z);

            // Rotación correcta (texto recto)
            nodoGO.transform.localRotation = Quaternion.Euler(90f, 90f, 0f);
            nodoGO.transform.localScale = Vector3.one * (escalaMapa * 0.1f);

            // Activar solo Base y Nodo_seguro
            if (v.nombreEscena == "Base" || v.nombreEscena == NOMBRE_OBJETIVO)
                nodoGO.SetActive(true);
            else
                nodoGO.SetActive(false);

            nodosInstanciados[v] = nodoGO;
        }

        // Crear las conexiones entre nodos
        foreach (var kv in posiciones)
        {
            Vertice padre = kv.Key;
            if (padre.izquierdo != null) CrearConexion(padre, padre.izquierdo, offset, false);
            if (padre.derecho != null) CrearConexion(padre, padre.derecho, offset, true);
        }
    }

    // -------------------------------
    void CrearConexion(Vertice padre, Vertice hijo, Vector2 offset, bool derecha)
    {
        if (!nodosInstanciados.ContainsKey(padre) || !nodosInstanciados.ContainsKey(hijo)) return;

        GameObject prefab = derecha ? conexionDerPrefab : conexionIzqPrefab;
        if (prefab == null) return;

        Vector3 pPos = nodosInstanciados[padre].transform.localPosition;
        Vector3 hPos = nodosInstanciados[hijo].transform.localPosition;

        GameObject conexion = Instantiate(prefab, transform);
        conexion.name = $"Conexion_{padre.nombreEscena}_a_{hijo.nombreEscena}";

        Vector3 mid = (pPos + hPos) / 2f;
        conexion.transform.localPosition = mid;

        Vector3 dir = hPos - pPos;
        float ang = Mathf.Atan2(dir.z, dir.x) * Mathf.Rad2Deg;
        conexion.transform.localRotation = Quaternion.Euler(90f, 90f, 0f);

        float longitud = dir.magnitude;
        conexion.transform.localScale = new Vector3(longitud * 0.5f * 0.15f * escalaMapa, 0.15f * escalaMapa, 0.15f * escalaMapa);

        conexion.SetActive(false);
        conexiones[(padre, hijo)] = conexion;
    }

    // -------------------------------
    public void ActualizarMapa()
    {
        foreach (var kv in nodosInstanciados)
        {
            Vertice nodo = kv.Key;
            GameObject go = kv.Value;
            if (go == null) continue;

            bool visible = nodo.nombreEscena == "Base" || nodo.nombreEscena == NOMBRE_OBJETIVO;

            if (!visible && GameManager.Instance != null && GameManager.arbol != null)
            {
                visible = GameManager.Instance.EscenaActual == nodo ||
                          EsAncestro(GameManager.arbol.raiz, nodo, GameManager.Instance.EscenaActual);
            }

            go.SetActive(visible);
        }

        // Mostrar conexiones solo si ambos nodos son visibles
        foreach (var kv in conexiones)
        {
            Vertice p = kv.Key.Item1;
            Vertice h = kv.Key.Item2;
            GameObject c = kv.Value;
            if (c != null)
            {
                bool padreVisible = nodosInstanciados[p].activeSelf;
                bool hijoVisible = nodosInstanciados[h].activeSelf;
                c.SetActive(padreVisible && hijoVisible);
            }
        }
    }

    bool EsAncestro(Vertice raiz, Vertice nodo, Vertice actual)
    {
        if (raiz == null) return false;
        if (raiz == actual) return true;
        return EsAncestro(raiz.izquierdo, nodo, actual) || EsAncestro(raiz.derecho, nodo, actual);
    }
}
