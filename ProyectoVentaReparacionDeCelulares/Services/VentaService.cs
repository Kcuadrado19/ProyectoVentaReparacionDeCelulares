using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProyectoVentaReparacionDeCelulares.Interface;
using ProyectoVentaReparacionDeCelulares.Models;

namespace ProyectoVentaReparacionDeCelulares.Services
{
    public class VentaService : IVentaService
    {
        private readonly VentaYReparacionDeCelulares1Entities _ctx =
            new VentaYReparacionDeCelulares1Entities();

        public IEnumerable<VentaMarketplace> GetAll() =>
            _ctx.VentaMarketplaces.ToList();

        public VentaMarketplace Get(int id) =>
            _ctx.VentaMarketplaces.Find(id);

        public VentaMarketplace Create(VentaMarketplace v)
        {
            _ctx.VentaMarketplaces.Add(v);
            _ctx.SaveChanges();
            return v;
        }
    }
}