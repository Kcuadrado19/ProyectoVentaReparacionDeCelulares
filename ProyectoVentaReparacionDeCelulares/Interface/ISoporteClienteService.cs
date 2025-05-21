using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProyectoVentaReparacionDeCelulares.Models;

namespace ProyectoVentaReparacionDeCelulares.Interface
{
    public interface ISoporteClienteService
    {
        IEnumerable<SoporteCliente> GetAll();
        SoporteCliente Get(int id);
        SoporteCliente Create(SoporteCliente soporte);
        SoporteCliente Update(SoporteCliente soporte);
        bool Delete(int id);
    }
}