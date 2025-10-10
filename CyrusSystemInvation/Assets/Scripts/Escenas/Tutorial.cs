using UnityEngine;
using UnityEngine.UI; 

public class OpenLink : MonoBehaviour
{
    public string URL = "https://www.google.com"; // Puedes cambiar esta URL por defecto

    public void OpenURLInBrowser()
    {
        Application.OpenURL(URL);
        Debug.Log("Abriendo enlace: " + URL); 
    }
}
