using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    public GameObject canvasAndroid;
    void Awake()
    {
        // Hide/show buttons based on platform
#if UNITY_STANDALONE || UNITY_EDITOR
        canvasAndroid.SetActive(false);
#elif UNITY_ANDROID || UNITY_IOS
        canvasAndroid.SetActive(true); 
        Debug.Log("Plataforma móvil detectada, mostrando canvas específico.");
#endif
    }



}
