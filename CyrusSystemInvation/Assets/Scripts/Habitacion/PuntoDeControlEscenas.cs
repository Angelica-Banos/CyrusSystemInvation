using UnityEngine;
using UnityEngine.SceneManagement;

public class PuntoDeControlEscenas : MonoBehaviour
{
    public int ID;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
        GameManager.Instance.EscenaActual = ID;

    }

}
