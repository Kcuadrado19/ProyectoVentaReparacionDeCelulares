jQuery(function () {
    $.get("https://proyectoventayreparacioncelulares.runasp.net/api/reparaciones", function (data) {
        let html = '<ul class="list-group">';
        data.forEach(r => {
            html += `<li class="list-group-item">
                <strong>${r.nombre_tecnico}</strong> reparó <strong>${r.producto}</strong> el <em>${r.fecha_reparacion}</em> - Estado: ${r.estado}
            </li>`;
        });
        html += '</ul>';
        $("#listaReparaciones").html(html);
    }).fail(() => {
        $("#listaReparaciones").html("<p class='text-danger'>No se pudo cargar el historial.</p>");
    });
});
