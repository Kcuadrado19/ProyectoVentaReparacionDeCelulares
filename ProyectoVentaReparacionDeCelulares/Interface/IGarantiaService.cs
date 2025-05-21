using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProyectoVentaReparacionDeCelulares.Models;
using ProyectoVentaReparacionDeCelulares.Services;

namespace ProyectoVentaReparacionDeCelulares.Interface
{
    public interface IGarantiaService
    {
        IEnumerable<Garantia> GetAll();
        Garantia Get(int id);
        Garantia Create(Garantia garantia);
        Garantia Update(Garantia garantia);
        bool Delete(int id);
    }
}