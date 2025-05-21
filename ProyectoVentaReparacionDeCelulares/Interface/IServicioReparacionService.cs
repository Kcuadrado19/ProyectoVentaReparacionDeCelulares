using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProyectoVentaReparacionDeCelulares.Models;

namespace ProyectoVentaReparacionDeCelulares.Interface
{
    public interface IServicioReparacionService
    {
        IEnumerable<ServicioReparacion> GetAll();
        ServicioReparacion Get(int id);
        ServicioReparacion Create(ServicioReparacion servicio);
        ServicioReparacion Update(ServicioReparacion servicio);
        bool Delete(int id);
    }
}