using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProyectoVentaReparacionDeCelulares.Models;
using ProyectoVentaReparacionDeCelulares.Services;

namespace ProyectoVentaReparacionDeCelulares.Interface
{
    public interface IFacturaVentaService
    {
        IEnumerable<FacturaMarketplace> GetAll();
        FacturaMarketplace Get(int id);
        FacturaMarketplace Create(FacturaMarketplace factura);
        FacturaMarketplace Update(FacturaMarketplace factura);
        bool Delete(int id);
    }
}