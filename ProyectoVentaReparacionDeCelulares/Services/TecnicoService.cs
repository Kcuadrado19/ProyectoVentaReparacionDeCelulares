using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProyectoVentaReparacionDeCelulares.Interface;
using ProyectoVentaReparacionDeCelulares.Models;

namespace ProyectoVentaReparacionDeCelulares.Services
{
    public class TecnicoService : ITecnicoService
    {
        private readonly VentaYReparacionDeCelulares1Entities _ctx = new VentaYReparacionDeCelulares1Entities();

        public IEnumerable<Tecnico> GetAll() =>
            _ctx.Tecnicoes.ToList();

        public Tecnico Get(int id) =>
            _ctx.Tecnicoes.Find(id);

        public Tecnico Create(Tecnico tecnico)
        {
            _ctx.Tecnicoes.Add(tecnico);
            _ctx.SaveChanges();
            return tecnico;
        }

        public Tecnico Update(Tecnico tecnico)
        {
            _ctx.Entry(tecnico).State = System.Data.Entity.EntityState.Modified;
            _ctx.SaveChanges();
            return tecnico;
        }

        public bool Delete(int id)
        {
            var t = _ctx.Tecnicoes.Find(id);
            if (t == null) return false;
            _ctx.Tecnicoes.Remove(t);
            _ctx.SaveChanges();
            return true;
        }
    }
}