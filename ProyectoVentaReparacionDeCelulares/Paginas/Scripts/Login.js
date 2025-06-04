class Login {
    constructor(username, password) {
        this.username = username;
        this.password = password;
    }
}

// Función para ejecutar comandos de servicio
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

// Función mejorada de login
async function Ingresar() {
    const btnIngresar = $("#btnIngresar");
    const btnText = $("#btnText");
    const loadingSpinner = $(".loading-spinner");
    const dvMensaje = $("#dvMensaje");

    // Validación de campos
    const usuario = $("#txtUsuario").val().trim();
    const clave = $("#txtClave").val().trim();

    if (!usuario || !clave) {
        mostrarMensaje("Por favor, completa todos los campos", "error");
        return;
    }

    // Estado de carga
    btnIngresar.prop("disabled", true);
    loadingSpinner.show();
    btnText.text("Iniciando sesión...");
    dvMensaje.hide();

    try {
        const BaseURL = "https://proyectoventayreparacioncelulares.runasp.net/";
        const URL = BaseURL + "api/auth/login";
        const login = new Login(usuario, clave);

        const Respuesta = await EjecutarComandoServicioRpta("POST", URL, login);

        if (Respuesta === undefined) {
            document.cookie = "token=0;path=/";
            mostrarMensaje("No se pudo conectar con el servicio. Verifica tu conexión a internet.", "error");
        } else if (Respuesta.token) {
            const extdays = 1;
            const d = new Date();
            d.setTime(d.getTime() + (extdays * 24 * 60 * 60 * 1000));
            document.cookie = `token=${Respuesta.token};expires=${d.toUTCString()};path=/`;

            mostrarMensaje("¡Autenticación exitosa! Redirigiendo...", "success");

            setTimeout(() => {
                window.location.href = "Marketplace.html";
            }, 1500);
        } else {
            mostrarMensaje("Usuario o contraseña incorrectos. Inténtalo nuevamente.", "error");
        }
    } catch (error) {
        console.error("Error durante el login:", error);
        mostrarMensaje("Error inesperado. Inténtalo más tarde.", "error");
    } finally {
        // Restaurar estado del botón
        setTimeout(() => {
            btnIngresar.prop("disabled", false);
            loadingSpinner.hide();
            btnText.text("Iniciar Sesión");
        }, 1000);
    }
}

// Función para mostrar mensajes
function mostrarMensaje(mensaje, tipo) {
    const dvMensaje = $("#dvMensaje");
    dvMensaje.removeClass("alert-success alert-danger");

    if (tipo === "success") {
        dvMensaje.addClass("alert-success");
    } else {
        dvMensaje.addClass("alert-danger");
    }

    dvMensaje.html(`<i class="fas fa-${tipo === 'success' ? 'check-circle' : 'exclamation-triangle'} mr-2"></i>${mensaje}`);
    dvMensaje.show();
}

// Toggle para mostrar/ocultar contraseña
$("#togglePassword").click(function () {
    const passwordField = $("#txtClave");
    const type = passwordField.attr("type") === "password" ? "text" : "password";
    passwordField.attr("type", type);

    $(this).removeClass("fa-eye fa-eye-slash");
    $(this).addClass(type === "password" ? "fa-eye" : "fa-eye-slash");
});

// Enter para enviar formulario
$("#loginForm input").keypress(function (e) {
    if (e.which === 13) {
        Ingresar();
    }
});

// Efecto de focus mejorado
$(".form-control").on("focus", function () {
    $(this).parent().find(".input-icon").css("color", "#fe889f");
}).on("blur", function () {
    $(this).parent().find(".input-icon").css("color", "#6c757d");
});

// Animación de carga de página
$(document).ready(function () {
    $("body").css("overflow", "hidden");
    setTimeout(() => {
        $("body").css("overflow", "auto");
    }, 600);
});