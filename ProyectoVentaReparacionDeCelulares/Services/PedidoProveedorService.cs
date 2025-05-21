using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProyectoVentaReparacionDeCelulares.Interface;
using ProyectoVentaReparacionDeCelulares.Models;

namespace ProyectoVentaReparacionDeCelulares.Services
{
    public class PedidoProveedorService : IPedidoProveedorService
    {
        private readonly VentaYReparacionDeCelulares1Entities _ctx =
            new VentaYReparacionDeCelulares1Entities();

        public IEnumerable<PedidoProveedor> GetAll()
        {
            return _ctx.PedidoProveedors.ToList();
        }

        public PedidoProveedor Get(int id)
        {
            return _ctx.PedidoProveedors.Find(id);
        }

        public PedidoProveedor Create(PedidoProveedor pedido)
        {
            _ctx.PedidoProveedors.Add(pedido);
            _ctx.SaveChanges();
            return pedido;
        }

        public PedidoProveedor Update(PedidoProveedor pedido)
        {
            _ctx.Entry(pedido).State = System.Data.Entity.EntityState.Modified;
            _ctx.SaveChanges();
            return pedido;
        }

        public bool Delete(int id)
        {
            var pedido = _ctx.PedidoProveedors.Find(id);
            if (pedido == null) return false;
            _ctx.PedidoProveedors.Remove(pedido);
            _ctx.SaveChanges();
            return true;
        }
    }
}