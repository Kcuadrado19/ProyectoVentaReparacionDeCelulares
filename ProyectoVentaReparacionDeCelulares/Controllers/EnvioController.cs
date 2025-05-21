using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ProyectoVentaReparacionDeCelulares.Models;
using ProyectoVentaReparacionDeCelulares.Interface;
using ProyectoVentaReparacionDeCelulares.Services;

namespace ProyectoVentaReparacionDeCelulares.Controllers
{
    [RoutePrefix("api/envios")]
    public class EnvioController : ApiController
    {
        private readonly IEnvioService _srv = new EnvioService();

        [HttpGet, Route("")]
        public IEnumerable<Envio> GetAll() => _srv.GetAll();

        [HttpGet, Route("{id:int}")]
        public IHttpActionResult Get(int id)
        {
            var e = _srv.Get(id);
            if (e == null) return NotFound();
            return Ok(e);
        }

        [HttpPost, Route("")]
        public IHttpActionResult Create(Envio envio)
        {
            var created = _srv.Create(envio);
            return Created("", created);
        }

        [HttpPut, Route("{id:int}")]
        public IHttpActionResult Update(int id, Envio envio)
        {
            if (id != envio.id_envio) return BadRequest();
            _srv.Update(envio);
            return StatusCode(HttpStatusCode.NoContent);
        }

        [HttpDelete, Route("{id:int}")]
        public IHttpActionResult Delete(int id)
        {
            if (!_srv.Delete(id)) return NotFound();
            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}
