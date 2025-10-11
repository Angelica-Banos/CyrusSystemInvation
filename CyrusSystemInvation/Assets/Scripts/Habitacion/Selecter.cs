using UnityEngine;

public class Selecter : MonoBehaviour
{
    LayerMask mask;
    public float distance = 3f;
    public GameObject textDetect;
    public Texture2D cursorDetect;
    GameObject ultimoObjeto=null;
    void Start()
    {
        mask = LayerMask.GetMask("Raycast Detect");
        textDetect.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, distance, mask))
        {
            DesSelect();
            SelectedObject(hit.transform);
            if (hit.collider.tag == "Objeto Interactivo")
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    hit.collider.GetComponent<Objetodeinteraccion>().Interaccion();
                }
            }
        }
        else { 
            DesSelect();
        }

    }
    void SelectedObject(Transform transform) { 
        textDetect.SetActive(true);
        ultimoObjeto = transform.gameObject;
    }
    void DesSelect() {
        if (ultimoObjeto) {
            textDetect.SetActive(false);
            ultimoObjeto = null;
        }
        textDetect.SetActive(false);
    }
}
