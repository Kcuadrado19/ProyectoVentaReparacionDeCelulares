using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProyectoVentaReparacionDeCelulares.Interface;
using ProyectoVentaReparacionDeCelulares.Models;

namespace ProyectoVentaReparacionDeCelulares.Services
{
    public class ImportacionProductoService : Interface.IImportacionProductoService
    {
        private readonly VentaYReparacionDeCelulares1Entities _ctx = new VentaYReparacionDeCelulares1Entities();

        public IEnumerable<ImportacionProducto> GetAll() =>
            _ctx.ImportacionProductoes.ToList();

        public ImportacionProducto Get(int id) =>
            _ctx.ImportacionProductoes.Find(id);

        public ImportacionProducto Create(ImportacionProducto ip)
        {
            _ctx.ImportacionProductoes.Add(ip);
            _ctx.SaveChanges();
            return ip;
        }

        public ImportacionProducto Update(ImportacionProducto ip)
        {
            _ctx.Entry(ip).State = System.Data.Entity.EntityState.Modified;
            _ctx.SaveChanges();
            return ip;
        }

        public bool Delete(int id)
        {
            var ip = _ctx.ImportacionProductoes.Find(id);
            if (ip == null) return false;
            _ctx.ImportacionProductoes.Remove(ip);
            _ctx.SaveChanges();
            return true;
        }
    }
}