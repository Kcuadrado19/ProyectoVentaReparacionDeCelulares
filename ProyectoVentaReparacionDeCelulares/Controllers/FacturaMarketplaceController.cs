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
    [RoutePrefix("api/facturas-venta")]
    public class FacturaMarketplaceController : ApiController
    {
        private readonly IFacturaVentaService _srv = new FacturaVentaService();

        // GET api/facturas-venta
        [HttpGet, Route("")]
        public IEnumerable<FacturaMarketplace> GetAll() => _srv.GetAll();

        // GET api/facturas-venta/5
        [HttpGet, Route("{id:int}")]
        public IHttpActionResult Get(int id)
        {
            var f = _srv.Get(id);
            if (f == null) return NotFound();
            return Ok(f);
        }

        // POST api/facturas-venta
        [HttpPost, Route("")]
        public IHttpActionResult Create(FacturaMarketplace factura)
        {
            var created = _srv.Create(factura);
            return Created("", created);
        }

        // PUT api/facturas-venta/5
        [HttpPut, Route("{id:int}")]
        public IHttpActionResult Update(int id, FacturaMarketplace factura)
        {
            if (id != factura.id_factura) return BadRequest();
            _srv.Update(factura);
            return StatusCode(HttpStatusCode.NoContent);
        }

        // DELETE api/facturas-venta/5
        [HttpDelete, Route("{id:int}")]
        public IHttpActionResult Delete(int id)
        {
            if (!_srv.Delete(id)) return NotFound();
            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}
