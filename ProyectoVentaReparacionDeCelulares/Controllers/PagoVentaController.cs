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
    [RoutePrefix("api/pagos-venta")]
    public class PagoVentaController : ApiController
    {
        private readonly IPagoVentaService _srv = new PagoVentaService();

        [HttpGet, Route("")]
        public IEnumerable<PagoVenta> GetAll() => _srv.GetAll();

        [HttpGet, Route("{id:int}")]
        public IHttpActionResult Get(int id)
        {
            var p = _srv.Get(id);
            if (p == null) return NotFound();
            return Ok(p);
        }

        [HttpPost, Route("")]
        public IHttpActionResult Create(PagoVenta pago)
        {
            var created = _srv.Create(pago);
            return Created("", created);
        }

        [HttpPut, Route("{id:int}")]
        public IHttpActionResult Update(int id, PagoVenta pago)
        {
            if (id != pago.id_pago) return BadRequest();
            _srv.Update(pago);
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
