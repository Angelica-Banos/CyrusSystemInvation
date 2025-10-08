using UnityEngine;

public class Selecter : MonoBehaviour
{
    LayerMask mask;
    public float distance = 1.5f;

    void Start()
    {
        mask = LayerMask.GetMask("Raycast Detect");
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, distance, mask)){
            if (hit.collider.tag == "Objeto Interactivo")
            { 
            if(Input.GetKeyDown(KeyCode.E))
                {
                    hit.collider.GetComponent<Objetodeinteraccion>().Interaccion();
                }
            }
        }
    }
}
