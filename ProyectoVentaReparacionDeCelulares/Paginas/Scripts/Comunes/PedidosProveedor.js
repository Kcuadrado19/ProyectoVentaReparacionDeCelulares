jQuery(function () {
    $("#dvMenu").load("../Paginas/Menu.html");
    cargarPedidos();

    $("#formPedido").on("submit", function (e) {
        e.preventDefault();
        guardarPedido();
    });

    // Eventos de filtrado en tiempo real
    $("#buscadorPedidos").on("input", aplicarFiltros);
    $("#estadoFiltro").on("change", aplicarFiltros);
});

function cargarPedidos() {
    // Mostrar indicador de carga
    $("#contenedorPedidos").html('<div class="loading-spinner"><i class="fas fa-spinner fa-spin fa-3x" style="color: #fe889f;"></i><p style="margin-top: 1rem; color: #6c757d;">Cargando pedidos...</p></div>');

    $.ajax({
        url: "https://proyectoventayreparacioncelulares.runasp.net/api/pedidos-proveedor",
        method: "GET",
        success: function (data) {
            const contenedor = $("#contenedorPedidos");
            contenedor.empty();

            if (data.length === 0) {
                contenedor.html('<div class="text-center" style="width: 100%; padding: 3rem;"><i class="fas fa-clipboard-list fa-3x mb-3" style="color: #fe889f;"></i><p style="color: #6c757d; font-size: 1.2rem;">No hay pedidos registrados</p></div>');
                return;
            }

            data.forEach(p => {
                const fechaFormateada = new Date(p.fecha_pedido).toLocaleDateString('es-ES');
                const estadoClass = p.estado === 'entregado' ? 'status-entregado' : 'status-pendiente';
                const estadoIcon = p.estado === 'entregado' ? 'fas fa-check-circle' : 'fas fa-clock';

                contenedor.append(`
                            <div class="card">
                                <h5 class="card-title">Pedido #${p.id_pedido}</h5>
                                <div class="info-item">
                                    <span class="info-label"><i class="fas fa-user-tie mr-2"></i>Proveedor:</span>
                                    <span class="info-value">${p.usuario?.nombre || 'No disponible'}</span>
                                </div>
                                <div class="info-item">
                                    <span class="info-label"><i class="fas fa-calendar mr-2"></i>Fecha:</span>
                                    <span class="info-value">${fechaFormateada}</span>
                                </div>
                                <div class="info-item">
                                    <span class="info-label"><i class="fas fa-flag mr-2"></i>Estado:</span>
                                    <span class="status-badge ${estadoClass}">
                                        <i class="${estadoIcon} mr-1"></i>${p.estado || 'Sin estado'}
                                    </span>
                                </div>
                                <div class="card-actions">
                                    <button class="btn btn-action btn-edit" onclick="editarPedido(${p.id_pedido})">
                                        <i class="fas fa-edit mr-1"></i>Editar
                                    </button>
                                    <button class="btn btn-action btn-delete" onclick="eliminarPedido(${p.id_pedido})">
                                        <i class="fas fa-trash mr-1"></i>Eliminar
                                    </button>
                                </div>
                            </div>
                        `);
            });
        },
        error: function () {
            $("#contenedorPedidos").html('<div class="error-message"><i class="fas fa-exclamation-triangle fa-3x mb-3" style="color: #dc3545;"></i><p style="color: #dc3545; font-size: 1.2rem;">Error al cargar los pedidos</p><button class="btn btn-outline-danger" onclick="cargarPedidos()">Reintentar</button></div>');
        }
    });
}

function mostrarFormulario() {
    $("#id_pedido").val("");
    $("#proveedor").val("");
    $("#fecha_pedido").val("");
    $("#estado").val("");
    $("#modalTitle").text('Nuevo Pedido a Proveedor');
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

    // Deshabilitar botón mientras se procesa
    const submitBtn = $("#formPedido").find('button[type="submit"]');
    const originalText = submitBtn.html();
    submitBtn.html('<i class="fas fa-spinner fa-spin mr-2"></i>Guardando...').prop('disabled', true);

    $.ajax({
        url: url,
        method: esNuevo ? "POST" : "PUT",
        contentType: "application/json",
        data: JSON.stringify(pedido),
        success: function () {
            $("#modalPedido").modal("hide");
            cargarPedidos();
            showNotification('Pedido guardado exitosamente', 'success');
        },
        error: function () {
            showNotification('Error al guardar el pedido', 'error');
        },
        complete: function () {
            submitBtn.html(originalText).prop('disabled', false);
        }
    });
}

function editarPedido(id) {
    $.get(`https://proyectoventayreparacioncelulares.runasp.net/api/pedidos-proveedor/${id}`)
        .done(function (p) {
            $("#id_pedido").val(p.id_pedido);
            $("#proveedor").val(p.usuario?.nombre || '');
            $("#fecha_pedido").val(p.fecha_pedido.split("T")[0]);
            $("#estado").val(p.estado);
            $("#modalTitle").text(`Editar Pedido #${p.id_pedido}`);
            $("#modalPedido").modal("show");
        })
        .fail(function () {
            showNotification('Error al cargar el pedido', 'error');
        });
}

function eliminarPedido(id) {
    if (confirm("⚠️ ¿Estás seguro de eliminar este pedido?\n\nEsta acción no se puede deshacer.")) {
        $.ajax({
            url: `https://proyectoventayreparacioncelulares.runasp.net/api/pedidos-proveedor/${id}`,
            method: "DELETE",
            success: function () {
                cargarPedidos();
                showNotification('Pedido eliminado exitosamente', 'success');
            },
            error: function () {
                showNotification('Error al eliminar el pedido', 'error');
            }
        });
    }
}

function aplicarFiltros() {
    const texto = $("#buscadorPedidos").val().toLowerCase().trim();
    const estado = $("#estadoFiltro").val().toLowerCase();
    const cards = $(".card");
    let visibleCount = 0;

    cards.each(function () {
        const card = $(this);
        const contenido = card.text().toLowerCase();

        const cumpleTexto = !texto || contenido.includes(texto);
        const cumpleEstado = !estado || contenido.includes(estado);
        const cumpleFiltros = cumpleTexto && cumpleEstado;

        if (cumpleFiltros) {
            card.show();
            visibleCount++;
        } else {
            card.hide();
        }
    });

    // Mostrar mensaje cuando no hay resultados
    $("#noResults").toggle(visibleCount === 0 && (texto !== "" || estado !== ""));
}

function showNotification(message, type) {
    const alertClass = type === 'success' ? 'alert-success' : 'alert-danger';
    const icon = type === 'success' ? 'fas fa-check-circle' : 'fas fa-exclamation-circle';

    const notification = $(`
                <div class="alert ${alertClass} alert-dismissible fade show" role="alert" style="position: fixed; top: 20px; right: 20px; z-index: 9999; min-width: 300px;">
                    <i class="${icon} mr-2"></i>${message}
                    <button type="button" class="close" data-dismiss="alert">
                        <span>&times;</span>
                    </button>
                </div>
            `);

    $('body').append(notification);

    // Auto-remove after 5 seconds
    setTimeout(() => {
        notification.alert('close');
    }, 5000);
}