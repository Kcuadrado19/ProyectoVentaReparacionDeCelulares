using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProyectoVentaReparacionDeCelulares.Models;

namespace ProyectoVentaReparacionDeCelulares.Interface
{
    public interface IFacturaCompraService
    {
        IEnumerable<FacturaCompra> GetAll();
        FacturaCompra Get(int id);
        FacturaCompra Create(FacturaCompra factura);
        FacturaCompra Update(FacturaCompra factura);
        bool Delete(int id);
    }
}