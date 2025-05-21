using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProyectoVentaReparacionDeCelulares.Interface;
using ProyectoVentaReparacionDeCelulares.Models;

namespace ProyectoVentaReparacionDeCelulares.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly VentaYReparacionDeCelulares1Entities _ctx =
            new VentaYReparacionDeCelulares1Entities();

        public Usuario Get(int id) =>
            _ctx.Usuarios.Find(id);

        public IEnumerable<Usuario> GetAll() =>
            _ctx.Usuarios.ToList();

        public Usuario Create(Usuario u)
        {
            _ctx.Usuarios.Add(u);
            _ctx.SaveChanges();
            return u;
        }

        public Usuario Update(Usuario u)
        {
            _ctx.Entry(u).State = System.Data.Entity.EntityState.Modified;
            _ctx.SaveChanges();
            return u;
        }

        public bool Delete(int id)
        {
            var u = _ctx.Usuarios.Find(id);
            if (u == null) return false;
            _ctx.Usuarios.Remove(u);
            _ctx.SaveChanges();
            return true;
        }
    }
}