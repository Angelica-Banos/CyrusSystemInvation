using System.Collections.Generic;
using UnityEngine;


public class ContraseñasBloques : MonoBehaviour
{
    [Header("Prefab y cantidad")]
    public GameObject prefab;         // Prefab con el script BloqueScript
    public int cantidad = 10;         // Total de bloques a generar

    [Header("Límites - puntos (ordenados)")]
    public List<Transform> boundaryPoints = new List<Transform>(); // vértices del área

    private List<GameObject> generados = new List<GameObject>();

    private void Start()
    {
        Generar();
    }

    [ContextMenu("Generar")]
    public void Generar()
    {
        Limpiar();

        if (prefab == null || boundaryPoints.Count < 3)
        {
            Debug.LogWarning("Asigna el prefab y al menos 3 puntos para definir el área.");
            return;
        }

        Bounds bounds = CalcularLimite(boundaryPoints);

        int generadosConCero = 0;

        for (int i = 0; i < cantidad; i++)
        {
            // Crear posición aleatoria dentro del área 3D
            Vector3 pos = PuntoAleatorio(bounds);

            // Instanciar el prefab
            GameObject obj = Instantiate(prefab, pos, Quaternion.identity);

            // Evitar posiciones idénticas si bounds Y es plano: añadir pequeño offset aleatorio en Y
            obj.transform.position = new Vector3(pos.x, pos.y + Random.Range(0.0f, 0.15f), pos.z);

            // Rotación fija aleatoria entre +Z, +X, -Z, -X
            float[] angulosY = new float[] { 0f, 90f, 180f, 270f };
            float ang = angulosY[Random.Range(0, angulosY.Length)];
            obj.transform.rotation = Quaternion.Euler(0f, ang, 0f);

            // Forzar tag y layer para que el Selecter lo detecte
            int layerIndex = LayerMask.NameToLayer("Raycast Detect");
            if (layerIndex != -1) obj.layer = layerIndex;
            obj.tag = "Objeto Interactivo";

            // Asegurar que tenga un collider 3D para los raycasts
            if (obj.GetComponent<Collider>() == null)
            {
                var bc = obj.AddComponent<BoxCollider>();
                // ajustar tamaño si hace falta: bc.size = ...
            }

            // Obtener el script y asignar número usando el método público
            BloqueScript bloque = obj.GetComponent<BloqueScript>();
            if (bloque == null)
            {
                Debug.LogWarning("Prefab instanciado no tiene BloqueScript.");
                Destroy(obj);
                continue;
            }
            if (generadosConCero < 4)
            {
                bloque.SetNumero(0);
                generadosConCero++;
            }
            else
            {
                bloque.SetNumero(Random.Range(1, 9));
            }

            generados.Add(obj);
        }

        // Asegurar que haya al menos 4 con número 0
        while (generadosConCero < 4 && generados.Count > 0)
        {
            int index = Random.Range(0, generados.Count);
            BloqueScript bloque = generados[index].GetComponent<BloqueScript>();
            if (bloque != null)
                bloque.SetNumero(0);
            generadosConCero++;
        }

        Debug.Log($"Generación completa. Bloques: {generados.Count}, con número 0: {generadosConCero}");
    }

    [ContextMenu("Limpiar")]
    public void Limpiar()
    {
        foreach (var obj in generados)
        {
            if (obj != null)
            {
#if UNITY_EDITOR
                if (!Application.isPlaying)
                    DestroyImmediate(obj);
                else
                    Destroy(obj);
#else
                Destroy(obj);
#endif
            }
        }
        generados.Clear();
    }

    // ---------- Utilidades ----------
    private Bounds CalcularLimite(List<Transform> puntos)
    {
        Vector3 min = puntos[0].position;
        Vector3 max = puntos[0].position;
        foreach (var t in puntos)
        {
            min = Vector3.Min(min, t.position);
            max = Vector3.Max(max, t.position);
        }
        Bounds b = new Bounds();
        b.SetMinMax(min, max);
        return b;
    }

    private Vector3 PuntoAleatorio(Bounds b)
    {
        float x = Random.Range(b.min.x, b.max.x);
        float y = Random.Range(b.min.y, b.max.y); // ahora también varía en Y
        float z = Random.Range(b.min.z, b.max.z);
        return new Vector3(x, y, z);
    }
}