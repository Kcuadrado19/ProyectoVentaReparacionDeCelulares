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
    [RoutePrefix("api/detalle-pedidos-proveedor")]
    public class DetallePedidoProveedorController : ApiController
    {
        private readonly IDetallePedidoProveedorService _srv = new DetallePedidoProveedorService();

        [HttpGet, Route("")]
        public IEnumerable<DetallePedidoProveedor> GetAll() => _srv.GetAll();

        [HttpGet, Route("{id:int}")]
        public IHttpActionResult Get(int id)
        {
            var d = _srv.Get(id);
            if (d == null) return NotFound();
            return Ok(d);
        }

        [HttpPost, Route("")]
        public IHttpActionResult Create(DetallePedidoProveedor detalle)
        {
            var created = _srv.Create(detalle);
            return Created("", created);
        }

        [HttpPut, Route("{id:int}")]
        public IHttpActionResult Update(int id, DetallePedidoProveedor detalle)
        {
            if (id != detalle.id_detalle_pedido) return BadRequest();
            _srv.Update(detalle);
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
