using System;
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
            try
            {
                if (login == null)
                    return BadRequest("Datos de login no enviados.");

                if (!_repo.ValidateCredentials(login.Username, login.Password))
                    return Unauthorized();

                if (string.IsNullOrEmpty(login.Username) || string.IsNullOrEmpty(login.Password))
                    return BadRequest("Usuario o contraseña vacíos.");


                var token = TokenGenerator.GenerateTokenJwt(login.Username);
                return Ok(new { token });
            }
            catch (Exception ex)
            {
                return InternalServerError(new Exception("Error en login: " + ex.Message));
            }
        }
    }
}


