using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProyectoVentaReparacionDeCelulares.Interface;
using ProyectoVentaReparacionDeCelulares.Models;

namespace ProyectoVentaReparacionDeCelulares.Services
{
    public class ProductoProveedorService : IProductoProveedorService
    {
        private readonly VentaYReparacionDeCelulares1Entities _ctx =
            new VentaYReparacionDeCelulares1Entities();

        public IEnumerable<ProductoProveedor> GetByProveedor(int proveedorId) =>
            _ctx.ProductoProveedors
                .Where(p => p.id_usuario_proveedor == proveedorId)
                .ToList();

        public ProductoProveedor Create(ProductoProveedor p)
        {
            _ctx.ProductoProveedors.Add(p);
            _ctx.SaveChanges();
            return p;
        }

        public IEnumerable<ProductoProveedor> GetAll()
        {
            return _ctx.ProductoProveedors.ToList();
        }


        public ProductoProveedor Update(ProductoProveedor p)
        {
            _ctx.Entry(p).State = System.Data.Entity.EntityState.Modified;
            _ctx.SaveChanges();
            return p;
        }

        public bool Delete(int id)
        {
            var p = _ctx.ProductoProveedors.Find(id);
            if (p == null) return false;
            _ctx.ProductoProveedors.Remove(p);
            _ctx.SaveChanges();
            return true;
        }
    }
}