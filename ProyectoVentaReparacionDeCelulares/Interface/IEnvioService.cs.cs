using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProyectoVentaReparacionDeCelulares.Models;
using ProyectoVentaReparacionDeCelulares.Services;

namespace ProyectoVentaReparacionDeCelulares.Interface
{
    public interface IEnvioService
    {
        IEnumerable<Envio> GetAll();
        Envio Get(int id);
        Envio Create(Envio envio);
        Envio Update(Envio envio);
        bool Delete(int id);
    }
}