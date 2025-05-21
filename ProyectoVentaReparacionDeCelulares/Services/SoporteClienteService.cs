using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProyectoVentaReparacionDeCelulares.Interface;
using ProyectoVentaReparacionDeCelulares.Models;

namespace ProyectoVentaReparacionDeCelulares.Services
{
    public class SoporteClienteService : ISoporteClienteService
    {
        private readonly VentaYReparacionDeCelulares1Entities _ctx = new VentaYReparacionDeCelulares1Entities();

        public IEnumerable<SoporteCliente> GetAll() =>
            _ctx.SoporteClientes.ToList();

        public SoporteCliente Get(int id) =>
            _ctx.SoporteClientes.Find(id);

        public SoporteCliente Create(SoporteCliente soporte)
        {
            _ctx.SoporteClientes.Add(soporte);
            _ctx.SaveChanges();
            return soporte;
        }

        public SoporteCliente Update(SoporteCliente soporte)
        {
            _ctx.Entry(soporte).State = System.Data.Entity.EntityState.Modified;
            _ctx.SaveChanges();
            return soporte;
        }

        public bool Delete(int id)
        {
            var s = _ctx.SoporteClientes.Find(id);
            if (s == null) return false;
            _ctx.SoporteClientes.Remove(s);
            _ctx.SaveChanges();
            return true;
        }
    }
}