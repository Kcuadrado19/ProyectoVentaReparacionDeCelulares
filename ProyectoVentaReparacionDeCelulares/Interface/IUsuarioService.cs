using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProyectoVentaReparacionDeCelulares.Models;

namespace ProyectoVentaReparacionDeCelulares.Interface
{
    public interface IUsuarioService
    {
        Usuario Get(int id);
        IEnumerable<Usuario> GetAll();
        Usuario Create(Usuario u);
        Usuario Update(Usuario u);
        bool Delete(int id);
    }
}