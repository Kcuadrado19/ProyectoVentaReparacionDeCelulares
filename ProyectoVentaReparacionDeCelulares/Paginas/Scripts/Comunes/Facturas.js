$(function () {
    cargarFacturas();

    $("#formFactura").on("submit", function (e) {
        e.preventDefault();
        const factura = {
            id_factura: $("#id_factura").val(),
            id_venta: $("#id_venta").val(),
            fecha_factura: $("#fecha_factura").val(),
            total_pagado: parseFloat($("#total_pagado").val()),
            total_proveedor: parseFloat($("#total_proveedor").val()),
            total_comision: parseFloat($("#total_comision").val())
        };

        const metodo = factura.id_factura ? "PUT" : "POST";
        const url = "https://proyectoventayreparacioncelulares.runasp.net/api/facturas-venta" +
            (factura.id_factura ? `/${factura.id_factura}` : "");

        // Deshabilitar botón mientras se procesa
        const submitBtn = $(this).find('button[type="submit"]');
        const originalText = submitBtn.html();
        submitBtn.html('<i class="fas fa-spinner fa-spin mr-2"></i>Guardando...').prop('disabled', true);

        $.ajax({
            url,
            method: metodo,
            contentType: "application/json",
            data: JSON.stringify(factura),
            success: () => {
                $("#modalFactura").modal("hide");
                cargarFacturas();
                // Mostrar notificación de éxito
                showNotification('Factura guardada exitosamente', 'success');
            },
            error: (xhr, status, error) => {
                showNotification('Error al guardar la factura', 'error');
                console.error('Error:', error);
            },
            complete: () => {
                submitBtn.html(originalText).prop('disabled', false);
            }
        });
    });

    // Búsqueda mejorada con funcionalidad para número de venta, factura y fecha
    $("#buscadorFactura").on("input", function () {
        const texto = $(this).val().toLowerCase().trim();
        const cards = $(".card");
        let visibleCount = 0;

        cards.each(function () {
            const card = $(this);
            const numeroFactura = card.find('.card-title').text().toLowerCase();
            const numeroVenta = card.find('.info-item:first .info-value').text().toLowerCase();
            const fecha = card.find('.info-item:nth-child(2) .info-value').text().toLowerCase();

            const coincide = numeroFactura.includes(texto) ||
                numeroVenta.includes(texto) ||
                fecha.includes(texto);

            if (coincide) {
                card.show();
                visibleCount++;
            } else {
                card.hide();
            }
        });

        // Mostrar mensaje cuando no hay resultados
        $("#noResults").toggle(visibleCount === 0 && texto !== "");
    });
});

function cargarFacturas() {
    // Mostrar indicador de carga
    $("#contenedorFacturas").html('<div class="text-center" style="width: 100%; padding: 3rem;"><i class="fas fa-spinner fa-spin fa-3x" style="color: #fe889f;"></i><p style="margin-top: 1rem; color: #6c757d;">Cargando facturas...</p></div>');

    $.get("https://proyectoventayreparacioncelulares.runasp.net/api/facturas-venta")
        .done(function (data) {
            const contenedor = $("#contenedorFacturas").empty();

            if (data.length === 0) {
                contenedor.html('<div class="text-center" style="width: 100%; padding: 3rem;"><i class="fas fa-file-invoice fa-3x mb-3" style="color: #fe889f;"></i><p style="color: #6c757d; font-size: 1.2rem;">No hay facturas registradas</p></div>');
                return;
            }

            data.forEach(f => {
                const fechaFormateada = new Date(f.fecha_factura).toLocaleDateString('es-ES');
                contenedor.append(`
                        <div class="card">
                            <h5 class="card-title">Factura #${f.id_factura}</h5>
                            <div class="info-item">
                                <span class="info-label"><i class="fas fa-shopping-cart mr-2"></i>Venta:</span>
                                <span class="info-value">${f.id_venta}</span>
                            </div>
                            <div class="info-item">
                                <span class="info-label"><i class="fas fa-calendar mr-2"></i>Fecha:</span>
                                <span class="info-value">${fechaFormateada}</span>
                            </div>
                            <div class="info-item">
                                <span class="info-label"><i class="fas fa-dollar-sign mr-2"></i>Total Pagado:</span>
                                <span class="info-value money-value">$${parseFloat(f.total_pagado).toLocaleString('es-CO')}</span>
                            </div>
                            <div class="info-item">
                                <span class="info-label"><i class="fas fa-truck mr-2"></i>Total Proveedor:</span>
                                <span class="info-value money-value">$${parseFloat(f.total_proveedor).toLocaleString('es-CO')}</span>
                            </div>
                            <div class="info-item">
                                <span class="info-label"><i class="fas fa-percentage mr-2"></i>Comisión:</span>
                                <span class="info-value money-value">$${parseFloat(f.total_comision).toLocaleString('es-CO')}</span>
                            </div>
                            <div class="card-actions">
                                <button class="btn btn-action btn-edit" onclick="editar(${f.id_factura})">
                                    <i class="fas fa-edit mr-1"></i>Editar
                                </button>
                                <button class="btn btn-action btn-delete" onclick="eliminar(${f.id_factura})">
                                    <i class="fas fa-trash mr-1"></i>Eliminar
                                </button>
                            </div>
                        </div>
                    `);
            });
        })
        .fail(function () {
            $("#contenedorFacturas").html('<div class="text-center" style="width: 100%; padding: 3rem;"><i class="fas fa-exclamation-triangle fa-3x mb-3" style="color: #dc3545;"></i><p style="color: #dc3545; font-size: 1.2rem;">Error al cargar las facturas</p><button class="btn btn-outline-danger" onclick="cargarFacturas()">Reintentar</button></div>');
        });
}

function mostrarFormulario() {
    $("#formFactura")[0].reset();
    $("#id_factura").val('');
    $("#modalTitle").text('Nueva Factura Marketplace');
    $("#modalFactura").modal("show");
}

function editar(id) {
    $.get(`https://proyectoventayreparacioncelulares.runasp.net/api/facturas-venta/${id}`)
        .done(function (f) {
            $("#id_factura").val(f.id_factura);
            $("#id_venta").val(f.id_venta);
            $("#fecha_factura").val(f.fecha_factura.split("T")[0]);
            $("#total_pagado").val(f.total_pagado);
            $("#total_proveedor").val(f.total_proveedor);
            $("#total_comision").val(f.total_comision);
            $("#modalTitle").text(`Editar Factura #${f.id_factura}`);
            $("#modalFactura").modal("show");
        })
        .fail(function () {
            showNotification('Error al cargar la factura', 'error');
        });
}

function eliminar(id) {
    // Modal de confirmación más elegante
    if (confirm("⚠️ ¿Estás seguro de eliminar esta factura?\n\nEsta acción no se puede deshacer.")) {
        $.ajax({
            url: `https://proyectoventayreparacioncelulares.runasp.net/api/facturas-venta/${id}`,
            method: "DELETE",
            success: function () {
                cargarFacturas();
                showNotification('Factura eliminada exitosamente', 'success');
            },
            error: function () {
                showNotification('Error al eliminar la factura', 'error');
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

    // Auto-remove after 5 seconds
    setTimeout(() => {
        notification.alert('close');
    }, 5000);
}

