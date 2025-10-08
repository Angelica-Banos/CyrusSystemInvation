using System.Collections.Generic;
using UnityEngine;

public class Correo : MonoBehaviour
{
    public int opElegida;
    public List<string> correosSeguros = new List<string>
    {
        "<soporte@**empresa-principal.com>",
        "<rrhh@**tu-organizacion.net>",
        "<no-responder@**banco-central.org>",
        "<mantenimiento@**sistema-interno.corp>",
        "<pedidos@**tienda-virtual.shop>",
        "<atencion@**marca-oficial.com>",
        "<juan.perez@**empresa-oficial.com>",
        "<noreply@**zoom-meeting.app>",
        "<rastreo@**mensajeria-express.com>",
        "<marketing@**tu-servicio-cloud.io>"
    };
    public List<string> correosSospechososImposibles = new List<string>
    {
        "<security_alert32@**gmail.com>",
        "<info@**amazonn-seguro.co>",
        "<p.a.y.p.a.l.service@**webmail.biz>",
        "<sistema-inactivo@**servicio-de-pago.xyz>",
        "<gerente@**actualizacion-datos.tk>",
        "<noreply_appleid_09@**hotmail.com>",
        "<banco-mexico@**soporte-financiero.online>",
        "<site.admin.001@**cuenta-soporte-web.com>",
        "<delivery@**paqueteria-urgente.org>",
        "<actualizacion_obligatoria@**security-service.cf>"
    };
    public List<string> asuntosSeguros = new List<string>
    {
        "Confirmacion de Pedido #12345 de [TuTienda]",
        "Factura de Servicios de [TuProveedor] - Octubre 2025",
        "Reunion de Equipo: Agenda y Enlaces (Miercoles 10:00 AM)",
        "Actualizacion Semanal del Proyecto 'Apolo'",
        "Aviso de Mantenimiento Programado de Sistemas",
        "Respuesta a su solicitud de soporte (Ticket #9876)",
        "Nuevo Documento Compartido: 'Plan Estratogico Q4'",
        "Confirmacion de Reserva en [Nombre de Aerolinea/Hotel]",
        "Notificacion de Deposito Directo de Nomina",
        "Cambios en la Politica de Privacidad de [Servicio Conocido]"
    };
    public List<string> asuntosSospechosos = new List<string>
    {
        "¡URGENTE! Su Cuenta Sera Suspendida si No Actua Ahora",
        "Se Ha Detectado Actividad de Inicio de Sesion Inusual en Su Cuenta",
        "Ganaste un iPhone 16 Pro Max ¡Reclamalo Ya!",
        "Pago Fallido: Actualice su Informacion de Facturacion Inmediatamente",
        "Tienes un Reembolso Pendiente (Haga Clic Aqui para Procesar)",
        "Notificacion: Un Archivo Adjunto Importante Requiere Su Atencion",
        "Alerta de Seguridad: Su Contrasena Ha Expirado - Clic para Restablecer",
        "Recibiste un Mensaje de Voz Misterioso",
        "CONFIDENCIAL: Revisar Este Documento (Solo Disponible Por 24 Horas)",
        "ultimo Aviso: Problema con su Entrega de Paquete (Incluye Enlace de Rastreo)"
    };
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
