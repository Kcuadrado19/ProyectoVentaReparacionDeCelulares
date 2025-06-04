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

async function Ingresar() {
    const BaseURL = "http://proyectoventayreparacioncelulares.runasp.net/";
    ///*let BaseURL = "http://localhost:5000/";*/
    //let BaseURL = "https://localhost:44365/";
    const URL = BaseURL + "api/auth/login";

    const login = new Login($("#txtUsuario").val(), $("#txtClave").val());

    const Respuesta = await EjecutarComandoServicioRpta("POST", URL, login);

    if (Respuesta === undefined) {
        document.cookie = "token=0;path=/";
        $("#dvMensaje").removeClass("alert alert-success").addClass("alert alert-danger")
            .html("No se pudo conectar con el servicio");
    } else if (Respuesta.token) {
        const extdays = 1;
        const d = new Date();
        d.setTime(d.getTime() + (extdays * 24 * 60 * 60 * 1000));
        document.cookie = `token=${Respuesta.token};expires=${d.toUTCString()};path=/`;

        $("#dvMensaje").removeClass("alert alert-danger").addClass("alert alert-success")
            .html("Autenticación exitosa");

        window.location.href = "Menu.html";
    } else {
        $("#dvMensaje").removeClass("alert alert-success").addClass("alert alert-danger")
            .html("Usuario o contraseña incorrectos");
    }
}
