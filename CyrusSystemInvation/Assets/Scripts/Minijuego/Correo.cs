using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

using UnityEngine.UI;
using UnityEngine.UIElements;

public class Correo : MonoBehaviour
{
    public GameManager gameManager;
    private int ReOpElegida = 0;
    private int AsOpElegida = 0;
    private int CuOpElegida = 0;

    private int ReOpCorrecta;
    private int AsOpCorrecta;
    private int CuOpCorrecta;

    public UnityEngine.UI.Button btnRe1;
    public UnityEngine.UI.Button btnRe2;
    public UnityEngine.UI.Button btnRe3;

    public UnityEngine.UI.Button btnAs1;
    public UnityEngine.UI.Button btnAs2;
    public UnityEngine.UI.Button btnAs3;

    public UnityEngine.UI.Button btnCu1;
    public UnityEngine.UI.Button btnCu2;
    public UnityEngine.UI.Button btnCu3;

    public Sprite imagenBtnSec;
    public Sprite imagenBtn;

    private Color colorTexto = new Color(0.8705882f, 0.7176471f, 0.5647059f); 
    private Color colorTextoSec = new Color(0.38f, 0.29f, 0.19f); 

    private List<string> correosSeguros = new List<string>
{
    "<soporte@banco-oficial.com>", 
    "<no-responder@banco-global.org>",
    "<avisos@su-banco-principal.net>",
    "<servicios-cliente@banco-nacional.com>",
    "<inversiones@banco-confianza.org>",
    "<notificaciones@banco-seguro.corp>",
    "<estado-cuenta@banco-regional.com>",
    "<contacto@banco-soluciones.net>",
    "<rrhh@banco-empleo.com>", 
    "<hipotecas@banco-credito.org>"
};
    private List<string> asuntosSeguros = new List<string>
{
    "Estado de Cuenta Mensual de [Banco] - Octubre 2025",
    "Notificacion: Cambio en el Horario de Atencion de Sucursales",
    "Confirmacion de Solicitud de Chequera (#12345)",
    "Aviso Legal: Actualizacion de Nuestros Terminos y Condiciones",
    "Informe Trimestral de Rendimiento de Inversiones",
    "Su Reclamo de Transaccion ha sido Solucionado (Caso #9876)",
    "Confirmacion de Deposito Directo de Nomina Recibido",
    "Invitacion: Seminario Web Gratuito Sobre Planificacion Financiera",
    "Confirmacion de Apertura de Nueva Cuenta de Ahorros",
    "Notificacion de Pago de Prestamo Hipotecario Vencimiento Proximo"
};
    private List<string> cuerposSeguros = new List<string>
{
    "Le enviamos el estado de cuenta de este mes como adjunto, en formato PDF.",
    "Gracias por actualizar su informacion de contacto. La verificacion ha finalizado.",
    "El banco le informa sobre un cambio en los terminos de servicio a partir de marzo.",
    "Su solicitud de un nuevo talonario de cheques ha sido procesada con exito.",
    "Hemos habilitado la opcion de transferencias internacionales instantaneas.",
    "Recibio un deposito directo de su empleador. Puede verlo en su historial online.",
    "Solo un recordatorio amistoso: su pago de prestamo hipotecario vence la proxima semana.",
    "Ya esta disponible nuestro boletin trimestral sobre inversiones en el portal.",
    "El horario de atencion de las sucursales cambiara a partir del proximo lunes.",
    "Le confirmamos la apertura de su nueva cuenta de ahorros, tal como lo solicito."
};

    private List<string> correosSospechosos = new List<string>
{
    "<security-alert-001@gmail.com>", 
    "<soporte@banco-oficiall.co>", 
    "<servicio-clientes@webmail.biz>", 
    "<banco-mexico@actualizacion-datos.tk>", 
    "<pago-urgente@servicio-banca.xyz>", 
    "<noreply_banco-mx@hotmail.com>", 
    "<gerente@portal-super-bancario-seguro.online>", 
    "<cuenta-inactiva@soporte-financiero.cf>", 
    "<XxtransferenciasxX@seguridad-bbva.org>", 
    "<alerta-banco-09@cuenta-soporte-web.pi>"
};
    private List<string> asuntosSospechosos = new List<string>
{
    "¡ALERTA URGENTE! Bloquearemos su Cuenta si No Inicia Sesion Ahora",
    "Su Tarjeta ha sido SUSPENDIDA - Haga Clic para la Reactivacion Inmediata",
    "Notificacion de Compra Grande NO Autorizada, Revise el Archivo Adjunto!!!",
    "Error en Su Transferencia de Fondos - Ingrese Su Clave para Corregir",
    "Reembolso de $5000 Pendiente (Se Requieren Sus Datos Bancarios)",
    "Ultimo Aviso: Su Informacion de Seguridad Ha Expirado - Actualice Hoy",
    "Detectamos Acceso ILEGAL a Su Cuenta Desde un Pais Extranjero",
    "Multa Impuesta Por el Banco Central - Debe Pagarla Inmediatamente",
    "Su PIN y Contraseña Han Sido Eliminados - Vuelva a Ingresarlos AQUI",
    "Tienes un Mensaje CONFIDENCIAL del Gerente del Banco Sobre Su Deuda"
};
    private List<string> cuerposSospechosos = new List<string>
{
    "URGENTE: Bloquearemos su cuenta si no verifica el PIN y la contraseña ahora mismo.",
    "Hemos detectado un intento de acceso a su cuenta desde el extranjero. Haga clic aqui.",
    "Se ha suspendido su tarjeta de débito. Para reactivarla, rellene este formulario.",
    "El banco requiere que actualice su número de seguridad social en el enlace adjunto.",
    "Error en una transferencia, debe ingresar sus datos de acceso para corregirlo hoy.",
    "Para evitar el cierre de su cuenta, ingrese su clave secreta en la web que le enviamos.",
    "Usted ha ganado un reembolso. Haga clic en el boton para recibir la transferencia.",
    "Debe descargar el archivo adjunto llamado 'Verificacion.zip' e instalar el software.",
    "Necesitamos confirmar sus credenciales bancarias para evitar un fraude masivo.",
    "Aviso final, su cuenta sera confiscada en 24 horas si no hace un pago de verificacion."
};
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameManager = GameManager.Instance;
        ReOpCorrecta = Random.Range(1, 4);
        AsOpCorrecta = Random.Range(1, 4);
        CuOpCorrecta = Random.Range(1, 4);
        ReCargarTexto(ReOpCorrecta);
        AsCargarTexto(AsOpCorrecta);
        CuCargarTexto(CuOpCorrecta);
    }

    
    public void ReBtnPresionado(int numOpcion)
    {
        ReOpElegida = numOpcion;
        switch(numOpcion)
        {
            case 1:
                btnRe1.image.sprite = imagenBtnSec;
                btnRe1.GetComponentInChildren<TextMeshProUGUI>().color = colorTextoSec;
                btnRe2.image.sprite = imagenBtn;
                btnRe2.GetComponentInChildren<TextMeshProUGUI>().color = colorTexto;
                btnRe3.image.sprite = imagenBtn;
                btnRe3.GetComponentInChildren<TextMeshProUGUI>().color = colorTexto;
                break;
            case 2:
                btnRe2.image.sprite = imagenBtnSec;
                btnRe2.GetComponentInChildren<TextMeshProUGUI>().color = colorTextoSec;
                btnRe1.image.sprite = imagenBtn;
                btnRe1.GetComponentInChildren<TextMeshProUGUI>().color = colorTexto;
                btnRe3.image.sprite = imagenBtn;
                btnRe3.GetComponentInChildren<TextMeshProUGUI>().color = colorTexto;
                break;
            case 3:
                btnRe3.image.sprite = imagenBtnSec;
                btnRe3.GetComponentInChildren<TextMeshProUGUI>().color = colorTextoSec;
                btnRe2.image.sprite = imagenBtn;
                btnRe2.GetComponentInChildren<TextMeshProUGUI>().color = colorTexto;
                btnRe1.image.sprite = imagenBtn;
                btnRe1.GetComponentInChildren<TextMeshProUGUI>().color = colorTexto;
                break;
            default:
                Debug.Log("ReBtnPresionado Opcion Default");
                break;
        }
    }
    public void AsBtnPresionado(int numOpcion)
    {
        AsOpElegida = numOpcion;
        switch (numOpcion)
        {
            case 1:
                btnAs1.image.sprite = imagenBtnSec;
                btnAs1.GetComponentInChildren<TextMeshProUGUI>().color = colorTextoSec;
                btnAs2.image.sprite = imagenBtn;
                btnAs2.GetComponentInChildren<TextMeshProUGUI>().color = colorTexto;
                btnAs3.image.sprite = imagenBtn;
                btnAs3.GetComponentInChildren<TextMeshProUGUI>().color = colorTexto;
                break;
            case 2:
                btnAs2.image.sprite = imagenBtnSec;
                btnAs2.GetComponentInChildren<TextMeshProUGUI>().color = colorTextoSec;
                btnAs1.image.sprite = imagenBtn;
                btnAs1.GetComponentInChildren<TextMeshProUGUI>().color = colorTexto;
                btnAs3.image.sprite = imagenBtn;
                btnAs3.GetComponentInChildren<TextMeshProUGUI>().color = colorTexto;
                break;
            case 3:
                btnAs3.image.sprite = imagenBtnSec;
                btnAs3.GetComponentInChildren<TextMeshProUGUI>().color = colorTextoSec;
                btnAs2.image.sprite = imagenBtn;
                btnAs2.GetComponentInChildren<TextMeshProUGUI>().color = colorTexto;
                btnAs1.image.sprite = imagenBtn;
                btnAs1.GetComponentInChildren<TextMeshProUGUI>().color = colorTexto;
                break;
            default:
                Debug.Log("AsBtnPresionado Opcion Default");
                break;
        }
    }
    public void CuBtnPresionado(int numOpcion)
    {
        CuOpElegida = numOpcion;
        switch (numOpcion)
        {
            case 1:
                btnCu1.image.sprite = imagenBtnSec;
                btnCu1.GetComponentInChildren<TextMeshProUGUI>().color = colorTextoSec;
                btnCu2.image.sprite = imagenBtn;
                btnCu2.GetComponentInChildren<TextMeshProUGUI>().color = colorTexto;
                btnCu3.image.sprite = imagenBtn;
                btnCu3.GetComponentInChildren<TextMeshProUGUI>().color = colorTexto;
                break;
            case 2:
                btnCu2.image.sprite = imagenBtnSec;
                btnCu2.GetComponentInChildren<TextMeshProUGUI>().color = colorTextoSec;
                btnCu1.image.sprite = imagenBtn;
                btnCu1.GetComponentInChildren<TextMeshProUGUI>().color = colorTexto;
                btnCu3.image.sprite = imagenBtn;
                btnCu3.GetComponentInChildren<TextMeshProUGUI>().color = colorTexto;
                break;
            case 3:
                btnCu3.image.sprite = imagenBtnSec;
                btnCu3.GetComponentInChildren<TextMeshProUGUI>().color = colorTextoSec;
                btnCu2.image.sprite = imagenBtn;
                btnCu2.GetComponentInChildren<TextMeshProUGUI>().color = colorTexto;
                btnCu1.image.sprite = imagenBtn;
                btnCu1.GetComponentInChildren<TextMeshProUGUI>().color = colorTexto;
                break;
            default:
                Debug.Log("AsBtnPresionado Opcion Default");
                break;
        }
    }

    private void ReCargarTexto(int opcionCorrecta)
    {
        switch (opcionCorrecta)
        {
            case 1:
                btnRe1.GetComponentInChildren<TextMeshProUGUI>().SetText(correosSospechosos[Random.Range(0,9)]);
                btnRe2.GetComponentInChildren<TextMeshProUGUI>().SetText(correosSeguros[Random.Range(0,9)]);
                btnRe3.GetComponentInChildren<TextMeshProUGUI>().SetText(correosSeguros[Random.Range(0,9)]);
                break;
            case 2:
                btnRe2.GetComponentInChildren<TextMeshProUGUI>().SetText(correosSospechosos[Random.Range(0, 9)]);
                btnRe1.GetComponentInChildren<TextMeshProUGUI>().SetText(correosSeguros[Random.Range(0, 9)]);
                btnRe3.GetComponentInChildren<TextMeshProUGUI>().SetText(correosSeguros[Random.Range(0, 9)]);
                break;
            case 3:
                btnRe3.GetComponentInChildren<TextMeshProUGUI>().SetText(correosSospechosos[Random.Range(0, 9)]);
                btnRe2.GetComponentInChildren<TextMeshProUGUI>().SetText(correosSeguros[Random.Range(0, 9)]);
                btnRe1.GetComponentInChildren<TextMeshProUGUI>().SetText(correosSeguros[Random.Range(0, 9)]);
                break;
            default:
                break;
        }
    }
    private void AsCargarTexto(int opcionCorrecta)
    {
        switch (opcionCorrecta)
        {
            case 1:
                btnAs1.GetComponentInChildren<TextMeshProUGUI>().SetText(asuntosSospechosos[Random.Range(0,9)]);
                btnAs2.GetComponentInChildren<TextMeshProUGUI>().SetText(asuntosSeguros[Random.Range(0,9)]);
                btnAs3.GetComponentInChildren<TextMeshProUGUI>().SetText(asuntosSeguros[Random.Range(0,9)]);
                break;
            case 2:
                btnAs2.GetComponentInChildren<TextMeshProUGUI>().SetText(asuntosSospechosos[Random.Range(0, 9)]);
                btnAs1.GetComponentInChildren<TextMeshProUGUI>().SetText(asuntosSeguros[Random.Range(0, 9)]);
                btnAs3.GetComponentInChildren<TextMeshProUGUI>().SetText(asuntosSeguros[Random.Range(0, 9)]);
                break;
            case 3:
                btnAs3.GetComponentInChildren<TextMeshProUGUI>().SetText(asuntosSospechosos[Random.Range(0, 9)]);
                btnAs2.GetComponentInChildren<TextMeshProUGUI>().SetText(asuntosSeguros[Random.Range(0, 9)]);
                btnAs1.GetComponentInChildren<TextMeshProUGUI>().SetText(asuntosSeguros[Random.Range(0, 9)]);
                break;
            default:
                break;
        }
    }
    private void CuCargarTexto(int opcionCorrecta)
    {
        switch (opcionCorrecta)
        {
            case 1:
                btnCu1.GetComponentInChildren<TextMeshProUGUI>().SetText(cuerposSospechosos[Random.Range(0,9)]);
                btnCu2.GetComponentInChildren<TextMeshProUGUI>().SetText(cuerposSeguros[Random.Range(0,9)]);
                btnCu3.GetComponentInChildren<TextMeshProUGUI>().SetText(cuerposSeguros[Random.Range(0,9)]);
                break;
            case 2:
                btnCu2.GetComponentInChildren<TextMeshProUGUI>().SetText(asuntosSospechosos[Random.Range(0, 9)]);
                btnCu1.GetComponentInChildren<TextMeshProUGUI>().SetText(cuerposSeguros[Random.Range(0, 9)]);
                btnCu3.GetComponentInChildren<TextMeshProUGUI>().SetText(cuerposSeguros[Random.Range(0, 9)]);
                break;
            case 3:
                btnCu3.GetComponentInChildren<TextMeshProUGUI>().SetText(cuerposSospechosos[Random.Range(0, 9)]);
                btnCu2.GetComponentInChildren<TextMeshProUGUI>().SetText(cuerposSeguros[Random.Range(0, 9)]);
                btnCu1.GetComponentInChildren<TextMeshProUGUI>().SetText(cuerposSeguros[Random.Range(0, 9)]);
                break;
            default:
                break;
        }
    }

    public void btnEnviarPresionado()
    {
               int respuestasCorrectas = 0;
        if(ReOpElegida == ReOpCorrecta) { respuestasCorrectas++; }
        if(AsOpElegida == AsOpCorrecta) { respuestasCorrectas++; }
        if(CuOpElegida == CuOpCorrecta) { respuestasCorrectas++; }
        if (respuestasCorrectas == 3)
        {
            gameManager.Correo();
            GameManager.Instance.MinijuegoActivo = false;
            GameObject playerCameraObject = GameObject.Find("PlayerCamera");
            //Cerrar juego copiado del script cerrarminijuego
            if (playerCameraObject != null)
            {
                Camera playerCam = playerCameraObject.GetComponent<Camera>();
                if (playerCam != null)
                {
                    playerCam.enabled = true;
                    Debug.Log("PlayerCamera re-enabled.");
                }
                else
                {
                    Debug.LogError("PlayerCamera object found, but it has no Camera component attached!");
                }
            }
            else
            {
                Debug.LogError("Could not find GameObject named 'PlayerCamera' in the active scenes. Check the name and scene loading status.");
            }
            GameObject.Find("BotonesAndroid").GetComponent<ControlesAndroid>().mostrar();
            SceneManager.UnloadScene(7);

            // Reanudar el juego 3D
            Time.timeScale = 1f;

            // Volver a bloquear el cursor (si tu juego lo usa)
            UnityEngine.Cursor.lockState = CursorLockMode.Locked;
            UnityEngine.Cursor.visible = false;

        }
        else
        {
            if(ReOpElegida != ReOpCorrecta)
            {
                switch (ReOpElegida)
                {
                    case 1:
                        btnRe1.image.sprite = imagenBtn;
                        btnRe1.GetComponentInChildren<TextMeshProUGUI>().color = colorTexto;
                        btnRe1.image.color = Color.red;
                        break;
                    case 2:
                        btnRe2.image.sprite = imagenBtn;
                        btnRe2.GetComponentInChildren<TextMeshProUGUI>().color = colorTexto;
                        btnRe2.image.color = Color.red;
                        break;
                    case 3:
                        btnRe3.image.sprite = imagenBtn;
                        btnRe3.GetComponentInChildren<TextMeshProUGUI>().color = colorTexto;
                        btnRe3.image.color = Color.red;
                        break;
                    default:
                        break;
                }
               
            }
            if(AsOpElegida != AsOpCorrecta)
            {
                switch (AsOpElegida)
                {
                    case 1:
                        btnAs1.image.sprite = imagenBtn;
                        btnAs1.GetComponentInChildren<TextMeshProUGUI>().color = colorTexto;
                        btnAs1.image.color = Color.red;
                        break;
                    case 2:
                        btnAs2.image.sprite = imagenBtn;
                        btnAs2.GetComponentInChildren<TextMeshProUGUI>().color = colorTexto;
                        btnAs2.image.color = Color.red;
                        break;
                    case 3:
                        btnAs3.image.sprite = imagenBtn;
                        btnAs3.GetComponentInChildren<TextMeshProUGUI>().color = colorTexto;
                        btnAs3.image.color = Color.red;
                        break;
                    default:
                        break;
                }
               
            }
            if(CuOpElegida != CuOpCorrecta)
            {
                switch (CuOpElegida)
                {
                    case 1:
                        btnCu1.image.sprite = imagenBtn;
                        btnCu1.GetComponentInChildren<TextMeshProUGUI>().color = colorTexto;
                        btnCu1.image.color = Color.red;
                        break;
                    case 2:
                        btnCu2.image.sprite = imagenBtn;
                        btnCu2.GetComponentInChildren<TextMeshProUGUI>().color = colorTexto;
                        btnCu2.image.color = Color.red;
                        break;
                    case 3:
                        btnCu3.image.sprite = imagenBtn;
                        btnCu3.GetComponentInChildren<TextMeshProUGUI>().color = colorTexto;
                        btnCu3.image.color = Color.red;
                        break;
                    default:
                        break;
                }
               
            }
        }
    }
}
