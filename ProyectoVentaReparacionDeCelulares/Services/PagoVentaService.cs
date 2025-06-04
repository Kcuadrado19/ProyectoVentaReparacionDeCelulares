using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProyectoVentaReparacionDeCelulares.Interface;
using ProyectoVentaReparacionDeCelulares.Models;

namespace ProyectoVentaReparacionDeCelulares.Services
{
    public class PagoVentaService : IPagoVentaService
    {
        private readonly VentaYReparacionDeCelulares1Entities _ctx = new VentaYReparacionDeCelulares1Entities();

        public IEnumerable<PagoVenta> GetAll() =>
            _ctx.PagoVentas.ToList();

        public PagoVenta Get(int id) =>
            _ctx.PagoVentas.Find(id);

        public PagoVenta Create(PagoVenta pago)
        {
            _ctx.PagoVentas.Add(pago);
            _ctx.SaveChanges();
            return pago;
        }

        public PagoVenta Update(PagoVenta pago)
        {
            var existing = _ctx.PagoVentas.Find(pago.id_pago);
            if (existing == null) return null;

            _ctx.Entry(existing).CurrentValues.SetValues(pago);
            _ctx.SaveChanges();
            return existing;
        }


        public bool Delete(int id)
        {
            var p = _ctx.PagoVentas.Find(id);
            if (p == null) return false;
            _ctx.PagoVentas.Remove(p);
            _ctx.SaveChanges();
            return true;
        }
    }
}