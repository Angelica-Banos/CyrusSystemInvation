using UnityEngine;

public class characterControler : MonoBehaviour
{
    [Header("Movimiento")]
    public float velocidad = 6f;
    public float gravedad = -9.81f;
    public float fuerzaSalto = 2f;

    [Header("Cámara")]
    public Transform camara;
    public float sensibilidadMouse = 100f;
    public float limiteVertical = 90f;

    private CharacterController controlador;
    private Vector3 velocidadJugador;
    private float rotacionX = 0f;

    void Start()
    {
        controlador = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked; // Bloquea el cursor al centro de la pantalla
    }

    void Update()
    {
        MoverJugador();
        MoverCamara();
    }

    void MoverJugador()
    {
        float x = Input.GetAxis("Horizontal"); // A, D
        float z = Input.GetAxis("Vertical");   // W, S

        Vector3 mover = transform.right * x + transform.forward * z;
        controlador.Move(mover * velocidad * Time.deltaTime);

        // Gravedad y salto
        if (controlador.isGrounded && velocidadJugador.y < 0)
            velocidadJugador.y = -2f; // mantiene al jugador pegado al suelo

        if (Input.GetButtonDown("Jump") && controlador.isGrounded)
            velocidadJugador.y = Mathf.Sqrt(fuerzaSalto * -2f * gravedad);

        velocidadJugador.y += gravedad * Time.deltaTime;
        controlador.Move(velocidadJugador * Time.deltaTime);
    }

    void MoverCamara()
    {
        float mouseX = Input.GetAxis("Mouse X") * sensibilidadMouse * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sensibilidadMouse * Time.deltaTime;

        rotacionX -= mouseY;
        rotacionX = Mathf.Clamp(rotacionX, -limiteVertical, limiteVertical);

        camara.localRotation = Quaternion.Euler(rotacionX, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
    }
}
