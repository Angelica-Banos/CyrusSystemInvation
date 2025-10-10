using UnityEngine;
using UnityEngine.SceneManagement;

public class AbrirMinijuego : MonoBehaviour
{
    public int indiceMinijuego = 7;      // Índice del minijuego (en Build Settings)
    public int indiceMinijuego2 = 8;      // Índice del minijuego (en Build Settings)
    public int nodo1 = 2, nodo2 = 3; // Nodos del árbol para desbloquear
    public int indiceSoloQuitar = 1;     // Índice de la escena donde solo se elimina algo
    public GameObject objetoAEliminar;   // Objeto a eliminar en esa escena
    public GameObject objetoAEliminar2;  //Otro Objeto a eliminar en esa escena
    public bool minijuegoCargado = false;
    public float distanciaInteraccion = 3f;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            // Obtener el índice de la escena actual
            int escenaActual = SceneManager.GetActiveScene().buildIndex;

            if (escenaActual == indiceSoloQuitar || escenaActual == 5)
            {
                // 🔴 En esta escena, solo eliminar un objeto
                if (objetoAEliminar != null)
                {
                    Destroy(objetoAEliminar);
                    Debug.Log("Objeto eliminado en la escena con índice " + escenaActual);
                }
                else
                {
                    if(objetoAEliminar2 != null)
                    {
                        Destroy(objetoAEliminar2);
                        Debug.Log("Objeto 2 eliminado en la escena con índice " + escenaActual);
                    }
                    else
                        Debug.LogWarning("No hay objeto asignado para eliminar en esta escena.");
                }
            }
            else
            {
                
                if (escenaActual == nodo1)
                {
                    // 🟢 En las demás escenas, abrir el minijuego
                    if (!minijuegoCargado)
                    {
                        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
                        if (Physics.Raycast(ray, out RaycastHit hit, distanciaInteraccion))
                        {
                            if (hit.collider.gameObject == gameObject)
                            {
                                SceneManager.LoadScene(indiceMinijuego, LoadSceneMode.Additive);
                                Camera.main.enabled = false;
                                minijuegoCargado = true;

                                Time.timeScale = 0f;
                                Cursor.lockState = CursorLockMode.None;
                                Cursor.visible = true;

                                Debug.Log("Minijuego cargado: escena " + indiceMinijuego);
                            }
                        }
                    }
                }
                else if (escenaActual == nodo2)
                {
                    // 🟢 En las demás escenas, abrir el minijuego
                    if (!minijuegoCargado)
                    {
                        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
                        if (Physics.Raycast(ray, out RaycastHit hit, distanciaInteraccion))
                        {
                            if (hit.collider.gameObject == gameObject)
                            {
                                SceneManager.LoadScene(indiceMinijuego2, LoadSceneMode.Additive);
                                Camera.main.enabled = false;
                                minijuegoCargado = true;
                                Time.timeScale = 0f;
                                Cursor.lockState = CursorLockMode.None;
                                Cursor.visible = true;
                                Debug.Log("Minijuego cargado: escena " + indiceMinijuego2);
                            }
                        }
                    }
                }
            }
        }
    }
}
