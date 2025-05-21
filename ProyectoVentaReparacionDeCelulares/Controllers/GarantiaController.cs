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
    [RoutePrefix("api/garantias")]
    public class GarantiaController : ApiController
    {
        private readonly IGarantiaService _srv = new GarantiaService();

        [HttpGet, Route("")]
        public IEnumerable<Garantia> GetAll() => _srv.GetAll();

        [HttpGet, Route("{id:int}")]
        public IHttpActionResult Get(int id)
        {
            var g = _srv.Get(id);
            if (g == null) return NotFound();
            return Ok(g);
        }

        [HttpPost, Route("")]
        public IHttpActionResult Create(Garantia garantia)
        {
            var created = _srv.Create(garantia);
            return Created("", created);
        }

        [HttpPut, Route("{id:int}")]
        public IHttpActionResult Update(int id, Garantia garantia)
        {
            if (id != garantia.id_garantia) return BadRequest();
            _srv.Update(garantia);
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
