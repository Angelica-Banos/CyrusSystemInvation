using UnityEngine;

public class Pez : MonoBehaviour
{
    public bool esFotoJugador = false; // ⚡ Indica si este pez fue creado a partir de una foto del jugador

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Caña"))
        {
            GameManagerPesca admin = FindFirstObjectByType<GameManagerPesca>();

            if (admin != null)
            {
                admin.Pescar(gameObject, esFotoJugador);
            }
            else
            {
                Destroy(gameObject); // fallback por seguridad
            }
        }
    }
}
