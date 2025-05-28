using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoVentaReparacionDeCelulares.Interface
{
    public interface IUserRepository
    {
        bool ValidateCredentials(string username, string password);
    }
}