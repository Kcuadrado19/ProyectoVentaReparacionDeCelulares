using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ProyectoVentaReparacionDeCelulares.Interface;
using ProyectoVentaReparacionDeCelulares.Models;
using ProyectoVentaReparacionDeCelulares.Services;

namespace ProyectoVentaReparacionDeCelulares.Controllers
{
    [RoutePrefix("api/facturas-compra")]
    public class FacturaCompraController : ApiController
    {
        private readonly IFacturaCompraService _srv = new FacturaCompraService();

        // GET api/facturas-compra
        [HttpGet, Route("")]
        public IEnumerable<FacturaCompra> GetAll() => _srv.GetAll();

        // GET api/facturas-compra/5
        [HttpGet, Route("{id:int}")]
        public IHttpActionResult Get(int id)
        {
            var f = _srv.Get(id);
            if (f == null) return NotFound();
            return Ok(f);
        }

        // POST api/facturas-compra
        [HttpPost, Route("")]
        public IHttpActionResult Create(FacturaCompra factura)
        {
            var created = _srv.Create(factura);
            return Created("", created);
        }

        // PUT api/facturas-compra/5
        [HttpPut, Route("{id:int}")]
        public IHttpActionResult Update(int id, FacturaCompra factura)
        {
            if (id != factura.id_factura_compra) return BadRequest();
            _srv.Update(factura);
            return StatusCode(HttpStatusCode.NoContent);
        }

        // DELETE api/facturas-compra/5
        [HttpDelete, Route("{id:int}")]
        public IHttpActionResult Delete(int id)
        {
            if (!_srv.Delete(id)) return NotFound();
            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}
