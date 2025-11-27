using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class MostrarMejoresTiempos : MonoBehaviour
{
    public UnityAPIManager apiManager;
    public TextMeshProUGUI marcadorTextMeshPro;
    private void Awake()
    {
        // Si no se asigna en el Inspector, intenta encontrarlo automáticamente
        if (apiManager == null)
        {
            apiManager = FindObjectOfType<UnityAPIManager>();
            if (apiManager == null)
            {
                Debug.LogError("UnityAPIManager no encontrado. Asegúrate de que esté en la escena.");
            }
        }

        if (apiManager != null && marcadorTextMeshPro != null)
        {
            CargarYMostrarMejoresTiempos();
        }
    }

    public void CargarYMostrarMejoresTiempos()
    {
        marcadorTextMeshPro.text = "Cargando mejores tiempos...";
        GetTopPlayersAsTextArray((textArray) =>
        {
            if (textArray == null || textArray.Length == 0)
            {
                marcadorTextMeshPro.text = "No hay datos de mejores tiempos disponibles.";
                return;
            }

            string textoFinal = string.Join("\n", textArray);

            marcadorTextMeshPro.text = textoFinal;
        });
    }

    public void GetTopPlayersAsTextArray(System.Action<string[]> callback)
    {
        if (apiManager == null)
        {
            callback(new string[] { "Error: Manager de API no está inicializado." });
            return;
        }

        // Llama al método del API Manager para obtener la lista de datos estructurados
        apiManager.GetTopPlayers((playerList) =>
        {
            // Una vez que el Manager responde con la lista de PlayerData, se convierte en texti.
            string[] textArray = ConvertPlayerListToTextArray(playerList);
            // Llama al callback con el array de strings resultante
            callback(textArray);
        });
    }


    private string[] ConvertPlayerListToTextArray(List<UnityAPIManager.PlayerData> players)
    {
        if (players == null || players.Count == 0)
        {
            return new string[] { "No se encontraron resultados o hubo un error en la API." };
        }
        List<string> textList = new List<string>();
        for (int i = 0; i < players.Count; i++)
        {
            //Jugador actual
            UnityAPIManager.PlayerData player = players[i];

            // Formato del tiempo
            string timeFormatted = FormatoTiempo(player.time);

            // i + 1 para reflejar la posición
            string playerText = $"{i + 1}. {player.name} - Tiempo: {timeFormatted} ({player.roomsVisited} Salas)";
            textList.Add(playerText);
        }

        // Devuelve el array de strings
        return textList.ToArray();
    }

    private string FormatoTiempo(float tiempo)
    {
        int minutes = Mathf.FloorToInt(tiempo / 60);
        float seconds = tiempo % 60;

        return $"{minutes:00}:{seconds:00.00}";
    }

    
}
