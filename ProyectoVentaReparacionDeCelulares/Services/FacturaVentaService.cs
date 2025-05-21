using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProyectoVentaReparacionDeCelulares.Interface;
using ProyectoVentaReparacionDeCelulares.Models;

namespace ProyectoVentaReparacionDeCelulares.Services
{
    public class FacturaVentaService : IFacturaVentaService
    {
        private readonly VentaYReparacionDeCelulares1Entities _ctx = new VentaYReparacionDeCelulares1Entities();

        public IEnumerable<FacturaMarketplace> GetAll() =>
            _ctx.FacturaMarketplaces.ToList();

        public FacturaMarketplace Get(int id) =>
            _ctx.FacturaMarketplaces.Find(id);

        public FacturaMarketplace Create(FacturaMarketplace factura)
        {
            _ctx.FacturaMarketplaces.Add(factura);
            _ctx.SaveChanges();
            return factura;
        }

        public FacturaMarketplace Update(FacturaMarketplace factura)
        {
            _ctx.Entry(factura).State = System.Data.Entity.EntityState.Modified;
            _ctx.SaveChanges();
            return factura;
        }

        public bool Delete(int id)
        {
            var f = _ctx.FacturaMarketplaces.Find(id);
            if (f == null) return false;
            _ctx.FacturaMarketplaces.Remove(f);
            _ctx.SaveChanges();
            return true;
        }
    }
}