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
    
    public partial class ServicioReparacion
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ServicioReparacion()
        {
            this.DetalleReparacions = new HashSet<DetalleReparacion>();
        }
    
        public int id_reparacion { get; set; }
        public int id_usuario_cliente { get; set; }
        public int id_sede { get; set; }
        public string descripcion_problema { get; set; }
        public Nullable<System.DateTime> fecha_ingreso { get; set; }
        public Nullable<System.DateTime> fecha_entrega { get; set; }
        public string estado { get; set; }
        public Nullable<decimal> costo { get; set; }
        public Nullable<int> id_usuario_admin { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DetalleReparacion> DetalleReparacions { get; set; }
        public virtual Sede Sede { get; set; }
        public virtual Usuario Usuario { get; set; }
        public virtual Usuario Usuario1 { get; set; }
    }
}
