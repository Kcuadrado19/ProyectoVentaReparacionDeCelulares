using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProyectoVentaReparacionDeCelulares.Interface;
using ProyectoVentaReparacionDeCelulares.Models;

namespace ProyectoVentaReparacionDeCelulares.Services
{
    public class ServicioReparacionService : IServicioReparacionService
    {
        private readonly VentaYReparacionDeCelulares1Entities _ctx =
            new VentaYReparacionDeCelulares1Entities();

        public IEnumerable<ServicioReparacion> GetAll()
        {
            return _ctx.ServicioReparacions.ToList();
        }

        public ServicioReparacion Get(int id)
        {
            return _ctx.ServicioReparacions.Find(id);
        }

        public ServicioReparacion Create(ServicioReparacion servicio)
        {
            _ctx.ServicioReparacions.Add(servicio);
            _ctx.SaveChanges();
            return servicio;
        }

        public ServicioReparacion Update(ServicioReparacion servicio)
        {
            _ctx.Entry(servicio).State = System.Data.Entity.EntityState.Modified;
            _ctx.SaveChanges();
            return servicio;
        }

        public bool Delete(int id)
        {
            var serv = _ctx.ServicioReparacions.Find(id);
            if (serv == null) return false;
            _ctx.ServicioReparacions.Remove(serv);
            _ctx.SaveChanges();
            return true;
        }
    }

}