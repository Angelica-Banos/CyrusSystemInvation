using System.Collections.Generic;
using UnityEngine;

public class Arbol : MonoBehaviour
{
    public Vertice raiz;
    public List<Vertice> vertices = new List<Vertice>();
    private int profundidadMaxima = 6;
    public Vertice nodobuscado;

    private void Awake()
    {
        GenerarArbolAleatorio();
        
    }

    public void GenerarArbolAleatorio()
    {
        // Crear raiz
        raiz = new Vertice("Base",0,0);
        vertices.Add(raiz);

        // Generar todo el arbol (maximo 4 niveles)
        GenerarRamas(raiz, 1);

        //Crear y ubicar el nodo buscado en el último nivel
        AgregarNodoBuscado();

    
    }

    private void GenerarRamas(Vertice padre, int nivel)
    {
        if (nivel >= profundidadMaxima) return;

        // Decide aleatoriamente si crea hijo izquierdo y/o derecho
         bool crearIzq = Random.value > 0.1f ;
         bool crearDer = Random.value > 0.1f ;


        if (crearIzq)
        {
            Vertice izq = new Vertice($"Nodo_{vertices.Count}",nivel,(padre.identificador*2)+1);
            padre.izquierdo = izq;
            vertices.Add(izq);
            izq.padre = padre;
            Debug.Log($"Creando nodo en nivel {nivel} con ID {izq.identificador} como hijo izquierdo de {padre.identificador}");  
            GenerarRamas(izq, nivel + 1);
        }

        if (crearDer)
        {
            Vertice der = new Vertice($"Nodo_{vertices.Count}",nivel,(padre.identificador * 2) + 2);
            padre.derecho = der;
            vertices.Add(der);
            der.padre = padre;
            Debug.Log($"Creando nodo en nivel {nivel} con ID {der.identificador} como hijo derecho de {padre.identificador}");
            GenerarRamas(der, nivel + 1);
        }
    }

    private void AgregarNodoBuscado()
    {
        // Obtener todos los vértices del último nivel
        List<Vertice> hojas = ObtenerHojas(raiz, 1, profundidadMaxima);

        while (hojas.Count == 0)
        {
            profundidadMaxima = profundidadMaxima -1;
            Debug.LogWarning("No se encontraron hojas en el último nivel, cambiando profundidad.");
            hojas = ObtenerHojas(raiz, 1, profundidadMaxima);
        }

        // Elegir una hoja aleatoria
        Vertice hojaElegida = hojas[Random.Range(0, hojas.Count)];

        // Crear el nodo central seguro como hijo de esa hoja
        Vertice nodoCentral = new Vertice("NodoCentralSeguro",profundidadMaxima,(hojaElegida.identificador*2));

        // Lo conectamos aleatoriamente como izquierdo o derecho
        if (Random.value > 0.5f)
        {
            hojaElegida.izquierdo = nodoCentral;
            nodoCentral.identificador = (hojaElegida.identificador * 2) + 1;
        }
        else
        {
            hojaElegida.derecho = nodoCentral;
            nodoCentral.identificador = (hojaElegida.identificador * 2) + 2;
        }
        vertices.Add(nodoCentral);
        nodoCentral.esNodoBuscado = true;
        nodobuscado = nodoCentral;
        Debug.Log($"Nodo Central Seguro conectado a la hoja {hojaElegida.nombreEscena}");
    }

    // Encuentra todas las hojas del último nivel
    private List<Vertice> ObtenerHojas(Vertice actual, int nivel, int maxNivel)
    {
        List<Vertice> hojas = new List<Vertice>();

        if (actual == null) return hojas;

        if (nivel == maxNivel)
        {
            hojas.Add(actual);
            return hojas;
        }
        
        hojas.AddRange(ObtenerHojas(actual.izquierdo, nivel + 1, maxNivel));
        hojas.AddRange(ObtenerHojas(actual.derecho, nivel + 1, maxNivel));

        return hojas;
    }

   

    public Vertice BuscarPorNombre(string nombre)
    {
        return vertices.Find(v => v.nombreEscena == nombre);
    }
}
