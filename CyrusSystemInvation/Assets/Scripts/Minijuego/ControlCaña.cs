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
        float vY = Input.GetAxis("Vertical");
        float vX = Input.GetAxis("Horizontal");
        Vector3 mov = new Vector3(vX, vY, 0f) * velocidad * Time.deltaTime;
        transform.Translate(mov);

        
        // estirar/encoger según dirección vertical
        Vector3 escala = transform.localScale;
        if (vY > 0.05f)
            escala.y = Mathf.Max(escalaMinima, escala.y - ajusteEscalaVel * Time.deltaTime);
        else if (vY < -0.05f)
            escala.y = Mathf.Min(escalaMaxima, escala.y + ajusteEscalaVel * Time.deltaTime);

        transform.localScale = escala;
    }
}
