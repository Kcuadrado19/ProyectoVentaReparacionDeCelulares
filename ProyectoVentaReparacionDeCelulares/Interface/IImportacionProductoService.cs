using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProyectoVentaReparacionDeCelulares.Models;

namespace ProyectoVentaReparacionDeCelulares.Interface
{
    public interface IImportacionProductoService
    {
        IEnumerable<ImportacionProducto> GetAll();
        ImportacionProducto Get(int id);
        ImportacionProducto Create(ImportacionProducto ip);
        ImportacionProducto Update(ImportacionProducto ip);
        bool Delete(int id);
    }
}