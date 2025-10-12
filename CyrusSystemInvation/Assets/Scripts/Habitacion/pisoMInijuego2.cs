using UnityEngine;

public class pisoMInijuego2 : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (GameManager.Instance.minijuego2win) { 
            GameManager.Instance.minijuego2win = false;
            Destroy(gameObject);
        }
    }

}
