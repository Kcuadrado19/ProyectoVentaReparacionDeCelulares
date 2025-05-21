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
    [RoutePrefix("api/importaciones-producto")]
    public class ImportacionProductoController : ApiController
    {
        private readonly ImportacionProductoService _srv = new ImportacionProductoService();

        [HttpGet, Route("")]
        public IEnumerable<ImportacionProducto> GetAll() => _srv.GetAll();

        [HttpGet, Route("{id:int}")]
        public IHttpActionResult Get(int id)
        {
            var ip = _srv.Get(id);
            if (ip == null) return NotFound();
            return Ok(ip);
        }

        [HttpPost, Route("")]
        public IHttpActionResult Create(ImportacionProducto ip)
        {
            var created = _srv.Create(ip);
            return Created("", created);
        }

        [HttpPut, Route("{id:int}")]
        public IHttpActionResult Update(int id, ImportacionProducto ip)
        {
            if (id != ip.id_importacion) return BadRequest();
            _srv.Update(ip);
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
