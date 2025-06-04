$(function () {
    cargarServicios();

    $("#formServicio").on("submit", function (e) {
        e.preventDefault();
        const servicio = {
            id_reparacion: $("#id_reparacion").val(),
            id_usuario_cliente: parseInt($("#id_usuario_cliente").val()),
            id_sede: parseInt($("#id_sede").val()),
            descripcion_problema: $("#descripcion_problema").val(),
            fecha_ingreso: $("#fecha_ingreso").val(),
            fecha_entrega: $("#fecha_entrega").val() || null,
            estado: $("#estado").val(),
            costo: parseFloat($("#costo").val()),
            id_usuario_admin: parseInt($("#id_usuario_admin").val())
        };

        const metodo = servicio.id_reparacion ? "PUT" : "POST";
        const url = "https://proyectoventayreparacioncelulares.runasp.net/api/servicios-reparacion" +
            (servicio.id_reparacion ? `/${servicio.id_reparacion}` : "");

        // Deshabilitar botón mientras se procesa
        const submitBtn = $(this).find('button[type="submit"]');
        const originalText = submitBtn.html();
        submitBtn.html('<i class="fas fa-spinner fa-spin mr-2"></i>Guardando...').prop('disabled', true);

        $.ajax({
            url,
            method: metodo,
            contentType: "application/json",
            data: JSON.stringify(servicio),
            success: () => {
                $("#modalServicio").modal("hide");
                cargarServicios();
                showNotification('Servicio guardado exitosamente', 'success');
            },
            error: (xhr, status, error) => {
                showNotification('Error al guardar el servicio', 'error');
                console.error('Error:', error);
            },
            complete: () => {
                submitBtn.html(originalText).prop('disabled', false);
            }
        });
    });

    // Búsqueda mejorada
    $("#buscadorServicio").on("input", function () {
        const texto = $(this).val().toLowerCase().trim();
        const cards = $(".card");
        let visibleCount = 0;

        cards.each(function () {
            const card = $(this);
            const contenido = card.text().toLowerCase();

            if (contenido.includes(texto)) {
                card.show();
                visibleCount++;
            } else {
                card.hide();
            }
        });

        $("#noResults").toggle(visibleCount === 0 && texto !== "");
    });
});

function cargarServicios() {
    $("#contenedorServicios").html('<div class="text-center" style="width: 100%; padding: 3rem;"><i class="fas fa-spinner fa-spin fa-3x" style="color: #fe889f;"></i><p style="margin-top: 1rem; color: #6c757d;">Cargando servicios...</p></div>');

    $.get("https://proyectoventayreparacioncelulares.runasp.net/api/servicios-reparacion")
        .done(function (data) {
            const contenedor = $("#contenedorServicios").empty();

            if (data.length === 0) {
                contenedor.html('<div class="text-center" style="width: 100%; padding: 3rem;"><i class="fas fa-tools fa-3x mb-3" style="color: #fe889f;"></i><p style="color: #6c757d; font-size: 1.2rem;">No hay servicios registrados</p></div>');
                return;
            }

            data.forEach(s => {
                const fechaIngreso = new Date(s.fecha_ingreso).toLocaleDateString('es-ES');
                const fechaEntrega = s.fecha_entrega ? new Date(s.fecha_entrega).toLocaleDateString('es-ES') : 'Pendiente';
                const statusClass = getStatusClass(s.estado);

                contenedor.append(`
                            <div class="card">
                                <h5 class="card-title">Reparación #${s.id_reparacion}</h5>
                                <div class="info-item">
                                    <span class="info-label"><i class="fas fa-user mr-2"></i>Cliente:</span>
                                    <span class="info-value">${s.id_usuario_cliente}</span>
                                </div>
                                <div class="info-item">
                                    <span class="info-label"><i class="fas fa-building mr-2"></i>Sede:</span>
                                    <span class="info-value">${s.id_sede}</span>
                                </div>
                                <div class="info-item">
                                    <span class="info-label"><i class="fas fa-flag mr-2"></i>Estado:</span>
                                    <span class="status-badge ${statusClass}">${s.estado}</span>
                                </div>
                                <div class="problema-description">
                                    <i class="fas fa-exclamation-triangle mr-2"></i>${s.descripcion_problema}
                                </div>
                                <div class="info-item">
                                    <span class="info-label"><i class="fas fa-calendar-plus mr-2"></i>Ingreso:</span>
                                    <span class="info-value">${fechaIngreso}</span>
                                </div>
                                <div class="info-item">
                                    <span class="info-label"><i class="fas fa-calendar-check mr-2"></i>Entrega:</span>
                                    <span class="info-value">${fechaEntrega}</span>
                                </div>
                                <div class="info-item">
                                    <span class="info-label"><i class="fas fa-dollar-sign mr-2"></i>Costo:</span>
                                    <span class="info-value money-value">$${parseFloat(s.costo).toLocaleString('es-CO')}</span>
                                </div>
                                <div class="card-actions">
                                    <button class="btn btn-action btn-edit" onclick="editar(${s.id_reparacion})">
                                        <i class="fas fa-edit mr-1"></i>Editar
                                    </button>
                                    <button class="btn btn-action btn-delete" onclick="eliminar(${s.id_reparacion})">
                                        <i class="fas fa-trash mr-1"></i>Eliminar
                                    </button>
                                </div>
                            </div>
                        `);
            });
        })
        .fail(function () {
            $("#contenedorServicios").html('<div class="text-center" style="width: 100%; padding: 3rem;"><i class="fas fa-exclamation-triangle fa-3x mb-3" style="color: #dc3545;"></i><p style="color: #dc3545; font-size: 1.2rem;">Error al cargar los servicios</p><button class="btn btn-outline-danger" onclick="cargarServicios()">Reintentar</button></div>');
        });
}

function getStatusClass(estado) {
    switch (estado.toLowerCase()) {
        case 'pendiente': return 'status-pendiente';
        case 'en proceso': return 'status-proceso';
        case 'completado': return 'status-completado';
        case 'entregado': return 'status-entregado';
        default: return 'status-pendiente';
    }
}

function mostrarFormulario() {
    $("#formServicio")[0].reset();
    $("#id_reparacion").val('');
    $("#modalTitle").text('Nuevo Servicio de Reparación');
    $("#modalServicio").modal("show");
}

function editar(id) {
    $.get(`https://proyectoventayreparacioncelulares.runasp.net/api/servicios-reparacion/${id}`)
        .done(function (s) {
            $("#id_reparacion").val(s.id_reparacion);
            $("#id_usuario_cliente").val(s.id_usuario_cliente);
            $("#id_sede").val(s.id_sede);
            $("#descripcion_problema").val(s.descripcion_problema);
            $("#fecha_ingreso").val(s.fecha_ingreso ? s.fecha_ingreso.split("T")[0] + "T" + s.fecha_ingreso.split("T")[1].substring(0, 5) : '');
            $("#fecha_entrega").val(s.fecha_entrega ? s.fecha_entrega.split("T")[0] + "T" + s.fecha_entrega.split("T")[1].substring(0, 5) : '');
            $("#estado").val(s.estado);
            $("#costo").val(s.costo);
            $("#id_usuario_admin").val(s.id_usuario_admin);
            $("#modalTitle").text(`Editar Servicio #${s.id_reparacion}`);
            $("#modalServicio").modal("show");
        })
        .fail(function () {
            showNotification('Error al cargar el servicio', 'error');
        });
}

function eliminar(id) {
    if (confirm("⚠️ ¿Estás seguro de eliminar este servicio?\n\nEsta acción no se puede deshacer.")) {
        $.ajax({
            url: `https://proyectoventayreparacioncelulares.runasp.net/api/servicios-reparacion/${id}`,
            method: "DELETE",
            success: function () {
                cargarServicios();
                showNotification('Servicio eliminado exitosamente', 'success');
            },
            error: function () {
                showNotification('Error al eliminar el servicio', 'error');
            }
        });
    }
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

    setTimeout(() => {
        notification.alert('close');
    }, 5000);
}