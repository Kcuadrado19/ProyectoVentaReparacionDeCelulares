using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProyectoVentaReparacionDeCelulares.Models;

namespace ProyectoVentaReparacionDeCelulares.Interface
{
    public interface IProductoProveedorService
    {
        IEnumerable<ProductoProveedor> GetByProveedor(int proveedorId);
        ProductoProveedor Create(ProductoProveedor p);
        ProductoProveedor Update(ProductoProveedor p);
        bool Delete(int id);
    }
}