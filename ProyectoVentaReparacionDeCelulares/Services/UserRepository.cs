using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using ProyectoVentaReparacionDeCelulares.Interface;
using System.Web.Helpers;
using ProyectoVentaReparacionDeCelulares.Models;
using BCrypt.Net;

namespace ProyectoVentaReparacionDeCelulares.Services
{
    public class UserRepository : IUserRepository
    {
        public bool ValidateCredentials(string username, string plainPassword)
        {
            using (var db = new VentaYReparacionDeCelulares1Entities())
            {
                var user = db.Usuarios.SingleOrDefault(u => u.usuario_login == username);
                if (user == null) return false;
                // Verifica hash (usa BCrypt o el método que uses)
                return BCrypt.Net.BCrypt.Verify(plainPassword, user.contrasena_hash);
            }
        }
    }
}