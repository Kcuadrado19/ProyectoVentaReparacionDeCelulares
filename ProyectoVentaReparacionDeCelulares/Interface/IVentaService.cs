using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProyectoVentaReparacionDeCelulares.Models;

namespace ProyectoVentaReparacionDeCelulares.Interface
{
    public interface IVentaService
    {
        IEnumerable<VentaMarketplace> GetAll();
        VentaMarketplace Get(int id);
        VentaMarketplace Create(VentaMarketplace v);
    }
}