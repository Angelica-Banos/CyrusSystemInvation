using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public UnityAPIManager apiManager; // Referencia al API manager
    public Button submitButton;

    public TMP_InputField nombre;
    public GameManager gameManager;

    [Header("Para el display")]
    public TMP_Text tiempo;
    public TMP_Text numNodos;
    public MostrarMejoresTiempos mostrarMejoresTiempos;
    void Start()
    {
        // Asignar función al botón
        try
        {
            gameManager = GameManager.Instance;
           
        }
        catch
        {
            Debug.LogWarning("No se pudo inicializar el UIManager correctamente.");
        }
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        submitButton.onClick.AddListener(OnSubmitButtonClicked);
        tiempo.SetText($"Tiempo: {FormatoTiempo(gameManager.segundosTranscurridos)}");
        numNodos.SetText($"Nodos visitados: {gameManager.cantidadNodosVisitados.ToString()}");

    }

    void OnSubmitButtonClicked()
    {
        apiManager.AddPlayer(nombre.text, gameManager.segundosTranscurridos, gameManager.cantidadNodosVisitados);
        Debug.Log("Datos enviados a la API");
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        mostrarMejoresTiempos.CargarYMostrarMejoresTiempos();
    }
    private string FormatoTiempo(float tiempo)
    {
        int minutes = Mathf.FloorToInt(tiempo / 60);
        float seconds = tiempo % 60;

        return $"{minutes:00}:{seconds:00.00}";
    }
}
