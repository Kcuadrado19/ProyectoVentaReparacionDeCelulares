using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ProyectoVentaReparacionDeCelulares.Clases;
using ProyectoVentaReparacionDeCelulares.Interface;
using ProyectoVentaReparacionDeCelulares.Services;

namespace ProyectoVentaReparacionDeCelulares.Controllers
{
    [RoutePrefix("api/auth")]
    public class LoginController : ApiController
    {
        
        private readonly IUserRepository _repo = new UserRepository();

        [HttpPost, Route("login")]
        public IHttpActionResult Login([FromBody] clsLogin login)
        {
            if (login == null || !_repo.ValidateCredentials(login.Username, login.Password))
                return Unauthorized();

            var token = TokenGenerator.GenerateTokenJwt(login.Username);
            return Ok(new { token });
        }
    }
}

