using UnityEngine;

public class Teletransportador : MonoBehaviour
{
    private Vector3 posicion = new Vector3(-0.391829f, -4.117f, -3.222641f);
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
