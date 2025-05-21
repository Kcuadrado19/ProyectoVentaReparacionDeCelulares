using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProyectoVentaReparacionDeCelulares.Models;

namespace ProyectoVentaReparacionDeCelulares.Interface
{
    public interface IPedidoProveedorService
    {
        IEnumerable<PedidoProveedor> GetAll();
        PedidoProveedor Get(int id);
        PedidoProveedor Create(PedidoProveedor pedido);
        PedidoProveedor Update(PedidoProveedor pedido);
        bool Delete(int id);
    }
}