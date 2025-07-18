﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ProyectoVentaReparacionDeCelulares.Clases;
using ProyectoVentaReparacionDeCelulares.Interface;
using ProyectoVentaReparacionDeCelulares.Models;
using ProyectoVentaReparacionDeCelulares.Services;

namespace ProyectoVentaReparacionDeCelulares.Controllers
{
    [RoutePrefix("api/usuarios")]
    public class UsuarioController : ApiController
    {
        private readonly IUsuarioService _srv = new UsuarioService();

        [HttpGet, Route("")]
        public IEnumerable<Usuario> GetAll() => _srv.GetAll();

        [HttpGet, Route("{id:int}")]
        public IHttpActionResult Get(int id)
        {
            var u = _srv.Get(id);
            if (u == null) return NotFound();
            return Ok(u);
        }

        [HttpPost, Route("")]
        public IHttpActionResult Create(Usuario u)
        {
            var cypher = new clsCypher { Password = u.contrasena_hash }; // Usa el campo temporalmente para la contraseña plana
            if (!cypher.CifrarClave()) return BadRequest("No se pudo cifrar la contraseña");

            u.contrasena_hash = cypher.PasswordCifrado;
            u.salt = cypher.Salt;

            var created = _srv.Create(u);
            return CreatedAtRoute("DefaultApi", new { controller = "usuarios", id = created.id_usuario }, created);
        }




        [HttpPut, Route("{id:int}")]
       public IHttpActionResult Update(int id, Usuario u)
       {
          if (id != u.id_usuario) 
              return BadRequest();

           _srv.Update(u);
           return Ok(new { message = "Usuario actualizado con éxito" });   
       }



        [HttpDelete, Route("{id:int}")]
       public IHttpActionResult Delete(int id)
       {
           if (!_srv.Delete(id)) 
               return NotFound();

           return Ok(new { message = "Usuario eliminado correctamente" });
       }




    }
}
