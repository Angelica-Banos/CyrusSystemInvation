using UnityEngine;

public class Teletransportador : MonoBehaviour
{
    private Vector3 posicion = new Vector3(1f, 1.5f, 0.5f);
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Transform playerTransform = other.transform;

            playerTransform.position = posicion;

            Debug.Log("Jugador movido a: " + posicion);
        }
    }
}
