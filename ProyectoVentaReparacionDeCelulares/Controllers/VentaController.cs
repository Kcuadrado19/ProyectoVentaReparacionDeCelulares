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
    [RoutePrefix("api/ventas")]
    public class VentaController : ApiController
    {
        private readonly IVentaService _srv = new VentaService();

        // GET api/ventas
        [HttpGet, Route("")]
        public IEnumerable<VentaMarketplace> GetAll() => _srv.GetAll();

        // GET api/ventas/5
        [HttpGet, Route("{id:int}")]
        public IHttpActionResult Get(int id)
        {
            var v = _srv.Get(id);
            if (v == null) return NotFound();
            return Ok(v);
        }

        // POST api/ventas
        [HttpPost, Route("")]
        public IHttpActionResult Create(VentaMarketplace v)
        {
            var created = _srv.Create(v);
            return Created("", created);
        }


        [HttpPut, Route("{id:int}")]
        public IHttpActionResult Update(int id, VentaMarketplace v)
        {
            if (id != v.id_venta)
                return BadRequest();

            _srv.Update(v);
            
            return Ok(new { message = "Venta actualizada con éxito" });
        }

        // DELETE api/ventas/{id}
        [HttpDelete, Route("{id:int}")]
        public IHttpActionResult Delete(int id)
        {
            if (!_srv.Delete(id))
                return NotFound();

            
            return Ok(new { message = "Venta eliminada correctamente" });
        }


    }
}
