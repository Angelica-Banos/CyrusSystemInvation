using UnityEngine;

[System.Serializable]
public class Vertice
{
    private static int contadorId = 0;

    public int id { get; private set; }
    public string nombreEscena; //Nombre del nodo, no del nodo en escenas
    public int profundidad;
    public int identificador;
    public Vertice izquierdo;
    public Vertice derecho;
    public Vertice padre;
    public bool completado = false;
    public bool esNodoBuscado = false;

    public Vertice(string nombreEscena,int prof, int identificador)
    {
        this.id = contadorId++;
        this.nombreEscena = nombreEscena;
        this.profundidad = prof;
        this.izquierdo = null;
        this.derecho = null;
        this.identificador = identificador;

    }
}
