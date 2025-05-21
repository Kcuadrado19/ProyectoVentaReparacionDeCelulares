using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProyectoVentaReparacionDeCelulares.Models;

namespace ProyectoVentaReparacionDeCelulares.Interface
{
    public interface ITecnicoService
    {
        IEnumerable<Tecnico> GetAll();
        Tecnico Get(int id);
        Tecnico Create(Tecnico tecnico);
        Tecnico Update(Tecnico tecnico);
        bool Delete(int id);
    }
}