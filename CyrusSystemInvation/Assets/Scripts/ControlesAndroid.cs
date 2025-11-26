using UnityEngine;

public class ControlesAndroid : MonoBehaviour
{

    public GameObject botones;
    public GameObject FirstPersonModularController;
    public GameObject menuPausa;
    void Start()
    {
        botones.SetActive(FirstPersonModularController.GetComponent<FirstPersonController>().useMobileInput);

        
    }
    void Update()
    {
       
    }

    public void ocultar()
    {
        botones.SetActive(false);
    }

    public void mostrar()
    {
        botones.SetActive(true);
    }
}
