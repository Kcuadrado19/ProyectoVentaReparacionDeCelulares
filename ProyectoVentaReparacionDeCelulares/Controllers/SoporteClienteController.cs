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
    [RoutePrefix("api/soporte-cliente")]
    public class SoporteClienteController : ApiController
    {
        private readonly ISoporteClienteService _srv = new SoporteClienteService();

        [HttpGet, Route("")]
        public IEnumerable<SoporteCliente> GetAll() => _srv.GetAll();

        [HttpGet, Route("{id:int}")]
        public IHttpActionResult Get(int id)
        {
            var s = _srv.Get(id);
            if (s == null) return NotFound();
            return Ok(s);
        }

        [HttpPost, Route("")]
        public IHttpActionResult Create(SoporteCliente soporte)
        {
            var created = _srv.Create(soporte);
            return Created("", created);
        }

        [HttpPut, Route("{id:int}")]
        public IHttpActionResult Update(int id, SoporteCliente soporte)
        {
            if (id != soporte.id_soporte) return BadRequest();
            _srv.Update(soporte);
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
