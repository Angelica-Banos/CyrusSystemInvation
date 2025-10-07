using UnityEngine;

[System.Serializable]
public class Vertice
{
    private static int contadorId = 0;

    public int id { get; private set; }
    public string nombreEscena;
    public Vertice izquierdo;
    public Vertice derecho;

    public Vertice(string nombreEscena)
    {
        this.id = contadorId++;
        this.nombreEscena = nombreEscena;
        this.izquierdo = null;
        this.derecho = null;
    }
}
