using UnityEngine;
using UnityEngine.SceneManagement;

public class PuntoDeControlEscenas : MonoBehaviour
{

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
        GameManager.Instance.ActualizarBase();

    }

}
