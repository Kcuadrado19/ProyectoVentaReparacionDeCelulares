using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProyectoVentaReparacionDeCelulares.Interface;
using ProyectoVentaReparacionDeCelulares.Models;

namespace ProyectoVentaReparacionDeCelulares.Services
{
    public class GarantiaService : IGarantiaService
    {
        private readonly VentaYReparacionDeCelulares1Entities _ctx = new VentaYReparacionDeCelulares1Entities();

        public IEnumerable<Garantia> GetAll() =>
            _ctx.Garantias.ToList();

        public Garantia Get(int id) =>
            _ctx.Garantias.Find(id);

        public Garantia Create(Garantia garantia)
        {
            _ctx.Garantias.Add(garantia);
            _ctx.SaveChanges();
            return garantia;
        }

        public Garantia Update(Garantia garantia)
        {
            _ctx.Entry(garantia).State = System.Data.Entity.EntityState.Modified;
            _ctx.SaveChanges();
            return garantia;
        }

        public bool Delete(int id)
        {
            var g = _ctx.Garantias.Find(id);
            if (g == null) return false;
            _ctx.Garantias.Remove(g);
            _ctx.SaveChanges();
            return true;
        }
    }
}