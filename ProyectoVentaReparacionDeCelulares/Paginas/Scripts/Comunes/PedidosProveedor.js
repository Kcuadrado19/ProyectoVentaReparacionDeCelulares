jQuery(function () {
    $("#dvMenu").load("../Paginas/Menu.html");
    cargarPedidos();

    $("#formPedido").on("submit", function (e) {
        e.preventDefault();
        guardarPedido();
    });
});

function cargarPedidos() {
    $.ajax({
        url: "https://proyectoventayreparacioncelulares.runasp.net/api/pedidos-proveedor",
        method: "GET",
        success: function (data) {
            const contenedor = $("#contenedorPedidos");
            contenedor.empty();

            data.forEach(p => {
                contenedor.append(`
                    <div class="card m-3 shadow-sm" style="width: 22rem;">
                        <div class="card-body">
                            <h5 class="card-title">Pedido #${p.id_pedido}</h5>
                            <p><strong>Proveedor:</strong> ${p.usuario?.nombre || 'No disponible'}</p>
                            <p><strong>Fecha:</strong> ${new Date(p.fecha_pedido).toLocaleDateString()}</p>
                            <p><strong>Estado:</strong> ${p.estado || 'Sin estado'}</p>
                            <button class="btn btn-primary btn-sm" onclick="editarPedido(${p.id_pedido})">Editar</button>
                            <button class="btn btn-danger btn-sm ml-2" onclick="eliminarPedido(${p.id_pedido})">Eliminar</button>
                        </div>
                    </div>
                `);
            });
        },
        error: function () {
            $("#contenedorPedidos").html("<p class='text-danger'>No se pudieron cargar los pedidos.</p>");
        }
    });
}

function mostrarFormulario() {
    $("#id_pedido").val("");
    $("#proveedor").val("");
    $("#fecha_pedido").val("");
    $("#estado").val("");
    $("#modalPedido").modal("show");
}

function guardarPedido() {
    const pedido = {
        id_pedido: $("#id_pedido").val(),
        id_usuario_proveedor: 1, // Ajusta si tienes selección de proveedor
        fecha_pedido: $("#fecha_pedido").val(),
        estado: $("#estado").val()
    };

    const esNuevo = pedido.id_pedido === "";
    const url = esNuevo
        ? "https://proyectoventayreparacioncelulares.runasp.net/api/pedidos-proveedor"
        : `https://proyectoventayreparacioncelulares.runasp.net/api/pedidos-proveedor/${pedido.id_pedido}`;

    $.ajax({
        url: url,
        method: esNuevo ? "POST" : "PUT",
        contentType: "application/json",
        data: JSON.stringify(pedido),
        success: function () {
            $("#modalPedido").modal("hide");
            cargarPedidos();
        },
        error: function () {
            alert("Error al guardar el pedido.");
        }
    });
}

function editarPedido(id) {
    $.get(`https://proyectoventayreparacioncelulares.runasp.net/api/pedidos-proveedor/${id}`, function (p) {
        $("#id_pedido").val(p.id_pedido);
        $("#proveedor").val(p.usuario?.nombre || '');
        $("#fecha_pedido").val(p.fecha_pedido.split("T")[0]);
        $("#estado").val(p.estado);
        $("#modalPedido").modal("show");
    });
}

function eliminarPedido(id) {
    if (confirm("¿Estás seguro de eliminar este pedido?")) {
        $.ajax({
            url: `https://proyectoventayreparacioncelulares.runasp.net/api/pedidos-proveedor/${id}`,
            method: "DELETE",
            success: function () {
                cargarPedidos();
            },
            error: function () {
                alert("Error al eliminar el pedido.");
            }
        });
    }
}

function aplicarFiltros() {
    const texto = $("#buscadorPedidos").val().toLowerCase();
    const estado = $("#estadoFiltro").val().toLowerCase();

    $("#contenedorPedidos .card").each(function () {
        const contenido = $(this).text().toLowerCase();
        const cumpleTexto = contenido.includes(texto);
        const cumpleEstado = !estado || contenido.includes(estado);

        $(this).toggle(cumpleTexto && cumpleEstado);
    });
}

// Eventos en tiempo real
$("#buscadorPedidos").on("input", aplicarFiltros);
$("#estadoFiltro").on("change", aplicarFiltros);



jQuery(function () {
    // Búsqueda de pedidos
    $("#buscadorPedidos").on("input", function () {
        const filtro = $(this).val().toLowerCase();

        $("#contenedorPedidos .card").each(function () {
            const texto = $(this).text().toLowerCase();
            $(this).toggle(texto.includes(filtro));
        });
    });
});
