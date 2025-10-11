using UnityEngine;
using UnityEngine.UI; 

public class OpenLink : MonoBehaviour
{
    public string URL = "https://www.google.com"; // URL por defecto, se cpuede cambiar en el Inspector

    public void OpenURLInBrowser()
    {
        Application.OpenURL(URL);
        Debug.Log("Abriendo enlace: " + URL); 
    }
}
