jQuery(function () {
    $.get("https://proyectoventayreparacioncelulares.runasp.net/api/facturas", function (data) {
        let html = '';
        data.forEach(f => {
            html += `<tr>
                <td>${f.id_factura}</td>
                <td>${f.nombre_cliente}</td>
                <td>${f.fecha}</td>
                <td>$${f.total}</td>
            </tr>`;
        });
        $("#cuerpoFacturas").html(html);
    }).fail(() => {
        $("#cuerpoFacturas").html("<tr><td colspan='4' class='text-danger text-center'>No se pudo cargar la información.</td></tr>");
    });
});
