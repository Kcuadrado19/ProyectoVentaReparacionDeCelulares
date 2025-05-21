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
    [RoutePrefix("api/tecnicos")]
    public class TecnicoController : ApiController
    {
        private readonly ITecnicoService _srv = new TecnicoService();

        [HttpGet, Route("")]
        public IEnumerable<Tecnico> GetAll() => _srv.GetAll();

        [HttpGet, Route("{id:int}")]
        public IHttpActionResult Get(int id)
        {
            var t = _srv.Get(id);
            if (t == null) return NotFound();
            return Ok(t);
        }

        [HttpPost, Route("")]
        public IHttpActionResult Create(Tecnico tecnico)
        {
            var created = _srv.Create(tecnico);
            return Created("", created);
        }

        [HttpPut, Route("{id:int}")]
        public IHttpActionResult Update(int id, Tecnico tecnico)
        {
            if (id != tecnico.id_tecnico) return BadRequest();
            _srv.Update(tecnico);
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
