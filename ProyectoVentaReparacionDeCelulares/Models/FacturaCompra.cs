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
    
    public partial class FacturaCompra
    {
        public int id_factura_compra { get; set; }
        public int id_pedido { get; set; }
        public Nullable<System.DateTime> fecha_factura { get; set; }
        public Nullable<decimal> monto_total { get; set; }
        public Nullable<int> id_usuario_admin { get; set; }
    
        public virtual PedidoProveedor PedidoProveedor { get; set; }
        public virtual Usuario Usuario { get; set; }
    }
}
