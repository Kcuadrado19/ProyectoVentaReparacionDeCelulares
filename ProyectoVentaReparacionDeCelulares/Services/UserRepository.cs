using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using ProyectoVentaReparacionDeCelulares.Interface;
using System.Web.Helpers;
using ProyectoVentaReparacionDeCelulares.Models;
using BCrypt.Net;
using System.Security.Cryptography;
using System.Text;


namespace ProyectoVentaReparacionDeCelulares.Services
{
    public class UserRepository : IUserRepository
    {
        //public bool ValidateCredentials(string username, string plainPassword)
        //{
        //    using (var db = new VentaYReparacionDeCelulares1Entities())
        //    {
        //        var user = db.Usuarios.SingleOrDefault(u => u.usuario_login == username);
        //        if (user == null) return false;
        //        // Verifica hash (usa BCrypt o el método que uses)
        //        return BCrypt.Net.BCrypt.Verify(plainPassword, user.contrasena_hash);
        //    }
        //}


        public bool ValidateCredentials(string username, string plainPassword)
        {
            using (var db = new VentaYReparacionDeCelulares1Entities())
            {
                var user = db.Usuarios.SingleOrDefault(u => u.usuario_login == username);
                if (user == null || user.salt == null || user.contrasena_hash == null)
                    return false;

                byte[] saltBytes = Convert.FromBase64String(user.salt);
                string hashedInput = HashPassword(plainPassword, saltBytes);

                return hashedInput == user.contrasena_hash;
            }
        }

        private string HashPassword(string password, byte[] salt)
        {
            using (var sha256 = SHA256.Create())
            {
                byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
                byte[] saltedPassword = new byte[passwordBytes.Length + salt.Length];

                Buffer.BlockCopy(passwordBytes, 0, saltedPassword, 0, passwordBytes.Length);
                Buffer.BlockCopy(salt, 0, saltedPassword, passwordBytes.Length, salt.Length);

                byte[] hashedBytes = sha256.ComputeHash(saltedPassword);

                byte[] hashedPasswordWithSalt = new byte[hashedBytes.Length + salt.Length];
                Buffer.BlockCopy(salt, 0, hashedPasswordWithSalt, 0, salt.Length);
                Buffer.BlockCopy(hashedBytes, 0, hashedPasswordWithSalt, salt.Length, hashedBytes.Length);

                return Convert.ToBase64String(hashedPasswordWithSalt);
            }
        }

    }
}