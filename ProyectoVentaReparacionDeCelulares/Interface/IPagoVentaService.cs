using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProyectoVentaReparacionDeCelulares.Models;
using ProyectoVentaReparacionDeCelulares.Interface;
using ProyectoVentaReparacionDeCelulares.Services;

namespace ProyectoVentaReparacionDeCelulares.Interface
{
    public interface IPagoVentaService
    {
        IEnumerable<PagoVenta> GetAll();
        PagoVenta Get(int id);
        PagoVenta Create(PagoVenta pago);
        PagoVenta Update(PagoVenta pago);
        bool Delete(int id);
    }
}