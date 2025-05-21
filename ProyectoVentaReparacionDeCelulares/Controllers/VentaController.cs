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
    }
}
