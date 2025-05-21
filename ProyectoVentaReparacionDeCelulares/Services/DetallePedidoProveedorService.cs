using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProyectoVentaReparacionDeCelulares.Interface;
using ProyectoVentaReparacionDeCelulares.Models;

namespace ProyectoVentaReparacionDeCelulares.Services
{
    public class DetallePedidoProveedorService : IDetallePedidoProveedorService
    {
        private readonly VentaYReparacionDeCelulares1Entities _ctx = new VentaYReparacionDeCelulares1Entities();

        public IEnumerable<DetallePedidoProveedor> GetAll() =>
            _ctx.DetallePedidoProveedors.ToList();

        public DetallePedidoProveedor Get(int id) =>
            _ctx.DetallePedidoProveedors.Find(id);

        public DetallePedidoProveedor Create(DetallePedidoProveedor detalle)
        {
            _ctx.DetallePedidoProveedors.Add(detalle);
            _ctx.SaveChanges();
            return detalle;
        }

        public DetallePedidoProveedor Update(DetallePedidoProveedor detalle)
        {
            _ctx.Entry(detalle).State = System.Data.Entity.EntityState.Modified;
            _ctx.SaveChanges();
            return detalle;
        }

        public bool Delete(int id)
        {
            var d = _ctx.DetallePedidoProveedors.Find(id);
            if (d == null) return false;
            _ctx.DetallePedidoProveedors.Remove(d);
            _ctx.SaveChanges();
            return true;
        }
    }
}