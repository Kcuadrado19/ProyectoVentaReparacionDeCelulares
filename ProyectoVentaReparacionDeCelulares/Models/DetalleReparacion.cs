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
    
    public partial class DetalleReparacion
    {
        public int id_detalle_reparacion { get; set; }
        public int id_reparacion { get; set; }
        public int id_tecnico { get; set; }
        public string actividad_realizada { get; set; }
        public Nullable<System.DateTime> fecha_actividad { get; set; }
    
        public virtual ServicioReparacion ServicioReparacion { get; set; }
        public virtual Tecnico Tecnico { get; set; }
    }
}
