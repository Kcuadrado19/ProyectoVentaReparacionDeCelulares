//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ProyectoVentaReparacionDeCelulares.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class DetallePedidoProveedor
    {
        public int id_detalle_pedido { get; set; }
        public int id_pedido { get; set; }
        public int id_producto_proveedor { get; set; }
        public Nullable<int> cantidad { get; set; }
        public Nullable<decimal> precio_unitario { get; set; }
    
        public virtual PedidoProveedor PedidoProveedor { get; set; }
        public virtual ProductoProveedor ProductoProveedor { get; set; }
    }
}
