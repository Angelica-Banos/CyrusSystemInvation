using UnityEngine;

// Simple componente que mueve hacia la derecha y recicla posición cuando sale
public class PezMover : MonoBehaviour
{
    private float velocidad;
    private float inicioX = 174f;
    private float finX = 194f;
    private float limiteY = 3.5f;

    void Start()
    {
        velocidad = Random.Range(1.2f, 2.2f);
        // asegúrate de mantener la misma X inicial si prefieres
    }

    void Update()
    {
        transform.Translate(Vector3.right * velocidad * Time.deltaTime);

        if (transform.position.x > finX)
        {
            transform.position = new Vector3(inicioX, Random.Range(-limiteY, limiteY), 0f);
        }
    }
}
