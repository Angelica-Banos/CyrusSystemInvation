using UnityEngine;

public class ControlCaña : MonoBehaviour
{
    [Header("Movimiento")]
    public float velocidad = 5f;
    public float limiteSuperior = 3.5f;
    public float limiteInferior = -3.5f;

    [Header("Estiramiento visual")]
    public float escalaMinima = 0.8f;
    public float escalaMaxima = 2.0f;
    public float ajusteEscalaVel = 1.5f;

    void Start()
    {
        // Asegurarse de que existe Rigidbody2D en kinematic (recomendado)
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody2D>();
            rb.bodyType = RigidbodyType2D.Kinematic;
            rb.gravityScale = 0f;
        }
    }

    void Update()
    {
        float vX = 0f;
        float vY = 0f;

        // Movimiento con las flechas del teclado
        if (Input.GetKey(KeyCode.UpArrow)) vY = 1f;
        if (Input.GetKey(KeyCode.DownArrow)) vY = -1f;
        if (Input.GetKey(KeyCode.RightArrow)) vX = 1f;
        if (Input.GetKey(KeyCode.LeftArrow)) vX = -1f;

        Vector3 mov = new Vector3(vX, vY, 0f).normalized * velocidad * Time.deltaTime;
        transform.Translate(mov);

        // Estiramiento visual según dirección vertical
        Vector3 escala = transform.localScale;
        if (vY > 0.05f)
            escala.y = Mathf.Max(escalaMinima, escala.y - ajusteEscalaVel * Time.deltaTime);
        else if (vY < -0.05f)
            escala.y = Mathf.Min(escalaMaxima, escala.y + ajusteEscalaVel * Time.deltaTime);

        transform.localScale = escala;

        // Limitar el movimiento en el eje Y
        Vector3 pos = transform.position;
        pos.y = Mathf.Clamp(pos.y, limiteInferior, limiteSuperior);
        transform.position = pos;
    }
}
