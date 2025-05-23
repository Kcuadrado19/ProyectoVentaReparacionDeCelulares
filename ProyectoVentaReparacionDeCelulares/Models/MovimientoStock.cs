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
    
    public partial class MovimientoStock
    {
        public int id_movimiento { get; set; }
        public int id_producto_proveedor { get; set; }
        public string tipo_movimiento { get; set; }
        public int cantidad { get; set; }
        public Nullable<System.DateTime> fecha_movimiento { get; set; }
        public string descripcion { get; set; }
        public Nullable<int> id_usuario_admin { get; set; }
    
        public virtual ProductoProveedor ProductoProveedor { get; set; }
        public virtual Usuario Usuario { get; set; }
    }
}
