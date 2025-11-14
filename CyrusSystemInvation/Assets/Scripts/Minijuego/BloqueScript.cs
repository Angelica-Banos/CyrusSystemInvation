using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


public class BloqueScript : Objetodeinteraccion
{
    // -1 = sin número asignado (texto vacío)
    public int numero = -1;
    public TMP_Text textoNumero; // opcional: asignar en el prefab; si no, se busca en hijos
    private SpriteRenderer spriteRend;

    // color base del texto (blanco)
    private Color textoBaseColor = Color.white;

    void Start()
    {
        gameManager = GameManager.Instance;

        spriteRend = GetComponent<SpriteRenderer>();

        if (textoNumero == null)
            textoNumero = GetComponentInChildren<TMP_Text>();

        // Estado inicial: sin número visible
        if (textoNumero != null)
        {
            if (numero == -1)
                textoNumero.text = "";
            else
            {
                textoNumero.text = numero.ToString();
                textoNumero.color = textoBaseColor;
            }
        }
    }

    // Llamar para asignar número y mostrarlo (texto en blanco por defecto)
    public void SetNumero(int n)
    {
        numero = n;
        if (textoNumero != null)
        {
            textoNumero.text = numero.ToString();
            textoNumero.color = textoBaseColor;
        }
    }

    // Si quieres poder "quitar" el número:
    public void ClearNumero()
    {
        numero = -1;
        if (textoNumero != null)
            textoNumero.text = "";
    }

    // Interacción: solo cambia el color del texto (verde si 0; rojo si no)
    public override void Interaccion()
    {
        if (textoNumero == null)
            textoNumero = GetComponentInChildren<TMP_Text>();

        if (SceneManager.GetActiveScene().buildIndex == 11)
        {
            if (numero == 0)
            {
                if (textoNumero != null) textoNumero.color = Color.green;
            }
            else
            {
                if (textoNumero != null) textoNumero.color = Color.red;
            }
        }
        else
        {
            if (numero == 0)
            {
                if (textoNumero != null) textoNumero.color = Color.green;
                gameManager?.contraseñas();
            }
            else
            {
                if (textoNumero != null) textoNumero.color = Color.red;
            }
        }
    }
}