using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public UnityAPIManager apiManager; // Referencia al API manager
    public Button submitButton;

    public TMP_InputField nombre;
    public GameManager gameManager;
    void Start()
    {
        // Asignar función al botón
        gameManager = GameManager.Instance;
        submitButton.onClick.AddListener(OnSubmitButtonClicked);
    }

    void OnSubmitButtonClicked()
    {
        apiManager.AddPlayer(nombre.text, gameManager.segundosTranscurridos, gameManager.cantidadNodosVisitados);
        Debug.Log("Datos enviados a la API");
    }
}
