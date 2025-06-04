async function Ingresar() {
    let BaseURL = "http://proyectoventayreparacioncelulares.runasp.net/";
   /* let BaseURL = "https://localhost:44365/";*/

    let URL = BaseURL + "api/auth/login";

    const login = new Login($("#txtUsuario").val(), $("#txtClave").val());

    const Respuesta = await EjecutarComandoServicioRpta("POST", URL, login);



    if (Respuesta === undefined) {
        document.cookie = "token=0;path=/";
        //Hubo un error al procesar el comando
        $("#dvMensaje").removeClass("alert alert-success");
        $("#dvMensaje").addClass("alert alert-danger");
        //$("#dvMensaje").html("No se pudo conectar con el servicio");
    }
    else {
        if (Respuesta[0].Autenticado == false) {
            document.cookie = "token=0;path=/";
            //Hubo un error al procesar el comando
            $("#dvMensaje").removeClass("alert alert-success");
            $("#dvMensaje").addClass("alert alert-danger");
            $("#dvMensaje").html(Respuesta[0].Mensaje);
        }
        else {
            const extdays = 5;
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
    constructor(username, password) {
        this.username = username;
        this.password = password;
    }
}

async function EjecutarComandoServicioRpta(metodo, url, datos) {
    try {
        const response = await fetch(url, {
            method: metodo,
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify(datos)
        });

        if (!response.ok) return undefined;
        return await response.json();
    } catch (error) {
        console.error("Error al conectar con el servicio:", error);
        return undefined;
    }
}