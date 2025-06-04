let todosLosProductos = [];
let productosFiltrados = [];

jQuery(function () {
    // Cargar menú si existe
    if ($("#dvMenu").length) {
        $("#dvMenu").load("../Paginas/Menu.html");
    }

    cargarProductos();
    configurarBuscador();
    configurarFiltros();
});

function cargarProductos() {
    $.get("https://proyectoventayreparacioncelulares.runasp.net/api/productos-proveedor")
        .done(function (data) {
            todosLosProductos = data;
            productosFiltrados = data;
            mostrarProductos(data);
            actualizarEstadisticas(data);
            $("#loadingMessage").hide();
        })
        .fail(function () {
            $("#loadingMessage").hide();
            $("#contenedorProductos").html(`
                        <div class="col-12 text-center text-danger">
                            <i class="fas fa-exclamation-triangle fa-3x mb-3"></i>
                            <h4>No se pudo cargar el catálogo</h4>
                            <p>Por favor, intenta recargar la página</p>
                            <button class="btn btn-primary" onclick="location.reload()">
                                <i class="fas fa-sync-alt mr-2"></i>Recargar
                            </button>
                        </div>
                    `);
        });
}

function mostrarProductos(productos) {
    const contenedor = $("#contenedorProductos");

    if (productos.length === 0) {
        $("#noProductsMessage").show();
        contenedor.empty();
        return;
    }

    $("#noProductsMessage").hide();
    contenedor.empty();

    productos.forEach((producto, index) => {
        const stockClass = getStockClass(producto.stock_disponible);
        const stockText = getStockText(producto.stock_disponible);

        const card = $(`
                    <div class="product-card fade-in" style="animation-delay: ${index * 0.1}s">
                        <div class="product-image">
                            <i class="fas fa-mobile-alt"></i>
                            ${producto.stock_disponible > 50 ? '<div class="product-badge">Stock Alto</div>' : ''}
                        </div>
                        <div class="product-body">
                            <h5 class="product-title">${producto.nombre_producto}</h5>
                            <p class="product-description">${producto.descripcion || 'Producto de alta calidad con garantía incluida'}</p>
                            
                            <div class="product-info">
                                <div class="info-row">
                                    <span class="info-label">
                                        <i class="fas fa-dollar-sign"></i> Precio
                                    </span>
                                    <span class="info-value price-value">$${formatearPrecio(producto.precio_unitario)}</span>
                                </div>
                                <div class="info-row">
                                    <span class="info-label">
                                        <i class="fas fa-boxes"></i> Stock
                                    </span>
                                    <span class="info-value ${stockClass}">${producto.stock_disponible} unidades</span>
                                </div>
                                <div class="info-row">
                                    <span class="info-label">
                                        <i class="fas fa-shield-alt"></i> Garantía
                                    </span>
                                    <span class="info-value">${producto.garantia_meses} meses</span>
                                </div>
                            </div>

                            <div class="product-actions">
                                <button class="btn-details" onclick="verDetalles(${producto.id_producto})">
                                    <i class="fas fa-eye mr-2"></i>Ver Detalles
                                </button>
                                <button class="btn-cart" onclick="agregarAlCarrito(${producto.id_producto})" title="Agregar al carrito">
                                    <i class="fas fa-shopping-cart"></i>
                                </button>
                            </div>
                        </div>
                    </div>
                `);

        contenedor.append(card);
    });
}

function configurarBuscador() {
    let searchTimeout;

    $("#searchInput").on('input', function () {
        clearTimeout(searchTimeout);
        searchTimeout = setTimeout(() => {
            buscarProductos();
        }, 300);
    });

    $("#searchInput").on('keypress', function (e) {
        if (e.which === 13) {
            buscarProductos();
        }
    });
}

function buscarProductos() {
    const termino = $("#searchInput").val().toLowerCase().trim();

    if (termino === '') {
        productosFiltrados = todosLosProductos;
    } else {
        productosFiltrados = todosLosProductos.filter(producto =>
            producto.nombre_producto.toLowerCase().includes(termino) ||
            (producto.descripcion && producto.descripcion.toLowerCase().includes(termino))
        );
    }

    aplicarFiltroActivo();
}

function configurarFiltros() {
    $('.filter-btn').click(function () {
        $('.filter-btn').removeClass('active');
        $(this).addClass('active');
        aplicarFiltroActivo();
    });
}

function aplicarFiltroActivo() {
    const filtroActivo = $('.filter-btn.active').data('filter');
    let productosAMostrar = productosFiltrados;

    if (filtroActivo !== 'all') {
        productosAMostrar = productosFiltrados.filter(producto => {
            switch (filtroActivo) {
                case 'celulares':
                    return producto.nombre_producto.toLowerCase().includes('celular') ||
                        producto.nombre_producto.toLowerCase().includes('móvil') ||
                        producto.nombre_producto.toLowerCase().includes('smartphone');
                case 'accesorios':
                    return producto.nombre_producto.toLowerCase().includes('accesorio') ||
                        producto.nombre_producto.toLowerCase().includes('cargador') ||
                        producto.nombre_producto.toLowerCase().includes('funda');
                case 'reparacion':
                    return producto.nombre_producto.toLowerCase().includes('reparación') ||
                        producto.nombre_producto.toLowerCase().includes('repuesto');
                case 'garantia':
                    return producto.garantia_meses >= 12;
                default:
                    return true;
            }
        });
    }

    mostrarProductos(productosAMostrar);
}

function actualizarEstadisticas(productos) {
    const totalProductos = productos.length;
    const totalStock = productos.reduce((sum, p) => sum + p.stock_disponible, 0);
    const categorias = new Set(productos.map(p => p.categoria || 'General')).size;

    $("#totalProductos").text(totalProductos);
    $("#totalCategorias").text(categorias);
    $("#totalStock").text(totalStock.toLocaleString());

    // Animación de conteo
    animarContadores();
}

function animarContadores() {
    $('.stat-number').each(function () {
        const $this = $(this);
        const target = parseInt($this.text().replace(/,/g, ''));
        if (isNaN(target)) return;

        let current = 0;
        const increment = target / 50;
        const timer = setInterval(() => {
            current += increment;
            if (current >= target) {
                current = target;
                clearInterval(timer);
            }
            $this.text(Math.floor(current).toLocaleString());
        }, 30);
    });
}

function getStockClass(stock) {
    if (stock > 20) return 'stock-high';
    if (stock > 5) return 'stock-medium';
    return 'stock-low';
}

function getStockText(stock) {
    if (stock > 20) return 'Stock Alto';
    if (stock > 5) return 'Stock Medio';
    return 'Stock Bajo';
}

function formatearPrecio(precio) {
    return new Intl.NumberFormat('es-CO', {
        minimumFractionDigits: 0,
        maximumFractionDigits: 0
    }).format(precio);
}

function verDetalles(idProducto) {
    // Aquí puedes implementar la lógica para mostrar detalles
    console.log('Ver detalles del producto:', idProducto);
    alert(`Viendo detalles del producto ${idProducto}`);
}

function agregarAlCarrito(idProducto) {
    // Aquí puedes implementar la lógica del carrito
    console.log('Agregar al carrito:', idProducto);

    // Animación de feedback
    event.target.classList.add('pulse');
    setTimeout(() => {
        event.target.classList.remove('pulse');
    }, 1000);

    // Mostrar mensaje de confirmación
    const toast = $(`
                <div class="alert alert-success alert-dismissible fade show position-fixed" 
                     style="top: 20px; right: 20px; z-index: 9999;">
                    <i class="fas fa-check-circle mr-2"></i>
                    Producto agregado al carrito
                    <button type="button" class="close" data-dismiss="alert">
                        <span>&times;</span>
                    </button>
                </div>
            `);

    $('body').append(toast);
    setTimeout(() => toast.alert('close'), 3000);
}

// Smooth scroll para mejorar la experiencia
$(window).scroll(function () {
    const scroll = $(window).scrollTop();
    $('.main-header').css('transform', `translateY(${scroll * 0.5}px)`);
});