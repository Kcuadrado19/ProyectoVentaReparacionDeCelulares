async function Ingresar() {
    //var: Variables globales a la página
    //let: Variables locales
    //const: Constantes locales
    let BaseURL = "http://appwebmie1820.runasp.net/";
    let URL = BaseURL + "api/Login/Ingresar";
    const login = new Login($("#txtUsuario").val(), $("#txtClave").val(), "");
    const Respuesta = await EjecutarComandoServicioRpta("POST", URL, login);
    if (Respuesta === undefined) {
        //se debe borrar la cookie con la informaición
        document.cookie = "token=0;path=/"; 
        //No obtuvo respuesta adecuada por parte del servidor
        //Se presenta el mensaje de error
        //Hubo un error al procesar el comando
        $("#dvMensaje").removeClass("alert alert-success");
        $("#dvMensaje").addClass("alert alert-danger");
        $("#dvMensaje").html("No se pudo conectar con el servicio");
    }
    else {
        if (Respuesta[0].Autenticado == false) {
            //No se autenticó en el servicio
            document.cookie = "token=0;path=/";
            $("#dvMensaje").removeClass("alert alert-success");
            $("#dvMensaje").addClass("alert alert-danger");
            $("#dvMensaje").html("No se pudo realizar el proceso de autenticación: " + Respuesta[0].Mensaje);
        }
        else {
            //Se conectó correctamente al servidor
            const extdays = 0.15;
            const d = new Date();
            d.setTime(d.getTime() + (extdays * 24 * 60 * 60 * 1000));
            let expires = ";expires=" + d.toUTCString();
            document.cookie = "token=" + Respuesta[0].Token + expires + ";path=/";
            $("#dvMensaje").removeClass("alert alert-danger");
            $("#dvMensaje").addClass("alert alert-success");
            $("#dvMensaje").html(Respuesta[0].Mensaje);
            document.cookie = "Perfil=" + Respuesta[0].Perfil;
            document.cookie = "Usuario=" + Respuesta[0].Usuario;
            window.location.href = Respuesta[0].PaginaInicio;
        }
    }
}

class Login {
    constructor(Usuario, Clave, PaginaSolicitud) {
        this.Usuario = Usuario;
        this.Clave = Clave;
        this.PaginaSolicitud = PaginaSolicitud;
    }
}