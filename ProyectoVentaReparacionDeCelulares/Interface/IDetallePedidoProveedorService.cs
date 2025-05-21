using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProyectoVentaReparacionDeCelulares.Models;
using ProyectoVentaReparacionDeCelulares.Services;

namespace ProyectoVentaReparacionDeCelulares.Interface
{
    public interface IDetallePedidoProveedorService
    {
        IEnumerable<DetallePedidoProveedor> GetAll();
        DetallePedidoProveedor Get(int id);
        DetallePedidoProveedor Create(DetallePedidoProveedor detalle);
        DetallePedidoProveedor Update(DetallePedidoProveedor detalle);
        bool Delete(int id);
    }
}