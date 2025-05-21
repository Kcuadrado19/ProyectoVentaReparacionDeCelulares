using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProyectoVentaReparacionDeCelulares.Interface;
using ProyectoVentaReparacionDeCelulares.Models;

namespace ProyectoVentaReparacionDeCelulares.Services
{
    public class EnvioService : IEnvioService
    {
        private readonly VentaYReparacionDeCelulares1Entities _ctx = new VentaYReparacionDeCelulares1Entities();

        public IEnumerable<Envio> GetAll() =>
            _ctx.Envios.ToList();

        public Envio Get(int id) =>
            _ctx.Envios.Find(id);

        public Envio Create(Envio envio)
        {
            _ctx.Envios.Add(envio);
            _ctx.SaveChanges();
            return envio;
        }

        public Envio Update(Envio envio)
        {
            _ctx.Entry(envio).State = System.Data.Entity.EntityState.Modified;
            _ctx.SaveChanges();
            return envio;
        }

        public bool Delete(int id)
        {
            var e = _ctx.Envios.Find(id);
            if (e == null) return false;
            _ctx.Envios.Remove(e);
            _ctx.SaveChanges();
            return true;
        }
    }
}