using UnityEngine;

public class minijuegoActivoComprovante : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameManager.Instance.MinijuegoActivo = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
