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
    [RoutePrefix("api/servicios-reparacion")]
    public class ServicioReparacionController : ApiController
    {
        private readonly IServicioReparacionService _srv = new ServicioReparacionService();

        // GET api/servicios-reparacion
        [HttpGet, Route("")]
        public IEnumerable<ServicioReparacion> GetAll() => _srv.GetAll();

        // GET api/servicios-reparacion/5
        [HttpGet, Route("{id:int}")]
        public IHttpActionResult Get(int id)
        {
            var servicio = _srv.Get(id);
            if (servicio == null) return NotFound();
            return Ok(servicio);
        }

        // POST api/servicios-reparacion
        [HttpPost, Route("")]
        public IHttpActionResult Create(ServicioReparacion servicio)
        {
            var created = _srv.Create(servicio);
            return Created("", created);
        }

        // PUT api/servicios-reparacion/5
        [HttpPut, Route("{id:int}")]
        public IHttpActionResult Update(int id, ServicioReparacion servicio)
        {
            if (id != servicio.id_reparacion) return BadRequest();
            _srv.Update(servicio);
            return StatusCode(HttpStatusCode.NoContent);
        }

        // DELETE api/servicios-reparacion/5
        [HttpDelete, Route("{id:int}")]
        public IHttpActionResult Delete(int id)
        {
            if (!_srv.Delete(id)) return NotFound();
            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}
