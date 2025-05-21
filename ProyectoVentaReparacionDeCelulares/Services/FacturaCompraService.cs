using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProyectoVentaReparacionDeCelulares.Interface;
using ProyectoVentaReparacionDeCelulares.Models;

namespace ProyectoVentaReparacionDeCelulares.Services
{
    public class FacturaCompraService : IFacturaCompraService
    {
        private readonly VentaYReparacionDeCelulares1Entities _ctx =
            new VentaYReparacionDeCelulares1Entities();

        public IEnumerable<FacturaCompra> GetAll()
        {
            return _ctx.FacturaCompras.ToList();
        }

        public FacturaCompra Get(int id)
        {
            return _ctx.FacturaCompras.Find(id);
        }

        public FacturaCompra Create(FacturaCompra factura)
        {
            _ctx.FacturaCompras.Add(factura);
            _ctx.SaveChanges();
            return factura;
        }

        public FacturaCompra Update(FacturaCompra factura)
        {
            _ctx.Entry(factura).State = System.Data.Entity.EntityState.Modified;
            _ctx.SaveChanges();
            return factura;
        }

        public bool Delete(int id)
        {
            var factura = _ctx.FacturaCompras.Find(id);
            if (factura == null) return false;
            _ctx.FacturaCompras.Remove(factura);
            _ctx.SaveChanges();
            return true;
        }
    }
}