using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using System.Collections.Generic;
using Newtonsoft.Json; // Necesitarás Json.NET para Unity


public class UnityAPIManager : MonoBehaviour
{
    private string apiUrl = "https://api-unity.laplataformapc.com/api/players";

    // Clase para enviar datos de jugador
    [System.Serializable]
    public class PlayerData
    {
        public string name;
        public float time;
        public int roomsVisited;
    }

    // Clase para recibir datos
    [System.Serializable]
    public class PlayerList
    {
        public List<PlayerData> players;
    }

    // POST: agregar jugador
    public void AddPlayer(string playerName, float time, int roomsVisited)
    {
        PlayerData player = new PlayerData { name = playerName, time = time, roomsVisited = roomsVisited };
        string jsonData = JsonConvert.SerializeObject(player);
        StartCoroutine(PostRequest(apiUrl, jsonData));
    }

    IEnumerator PostRequest(string url, string json)
    {
        UnityWebRequest request = new UnityWebRequest(url, "POST");
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(json);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

#if UNITY_2020_1_OR_NEWER
        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
#else
        if(request.isNetworkError || request.isHttpError)
#endif
        {
            Debug.LogError("Error POST: " + request.error);
        }
        else
        {
            Debug.Log("Jugador agregado correctamente");
        }
    }

    // GET: obtener top jugadores
    public void GetTopPlayers(System.Action<List<PlayerData>> callback)
    {
        StartCoroutine(GetRequest(apiUrl + "/top", callback));
    }

    IEnumerator GetRequest(string url, System.Action<List<PlayerData>> callback)
    {
        UnityWebRequest request = UnityWebRequest.Get(url);
        yield return request.SendWebRequest();

#if UNITY_2020_1_OR_NEWER
        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
#else
        if(request.isNetworkError || request.isHttpError)
#endif
        {
            Debug.LogError("Error GET: " + request.error);
        }
        else
        {
            // Deserialize JSON
            var players = JsonConvert.DeserializeObject<List<PlayerData>>(request.downloadHandler.text);
            callback(players);
        }
    }
}
