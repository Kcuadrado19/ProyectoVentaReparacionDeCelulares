jQuery(function () {
    $("#dvMenu").load("../Paginas/Menu.html");

    $.ajax({
        url: "https://proyectoventayreparacioncelulares.runasp.net/api/productos", // ajusta si es necesario
        method: "GET",
        success: function (productos) {
            let html = '';
            productos.forEach(p => {
                html += `
                    <div class="col-md-4 mb-3">
                        <div class="card">
                            <div class="card-body">
                                <h5 class="card-title">${p.nombre_producto}</h5>
                                <p class="card-text">Precio: $${p.precio}</p>
                                <p class="card-text">Categoría: ${p.categoria}</p>
                                <p class="card-text">Stock: ${p.cantidad}</p>
                            </div>
                        </div>
                    </div>`;
            });
            $("#contenedorProductos").html(html);
        },
        error: function () {
            $("#contenedorProductos").html("<p>Error cargando productos.</p>");
        }
    });
});
