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
    [RoutePrefix("api/pedidos-proveedor")]
    public class PedidoProveedorController : ApiController
    {
        private readonly IPedidoProveedorService _srv = new PedidoProveedorService();

        // GET api/pedidos-proveedor
        [HttpGet, Route("")]
        public IEnumerable<PedidoProveedor> GetAll() => _srv.GetAll();

        // GET api/pedidos-proveedor/5
        [HttpGet, Route("{id:int}")]
        public IHttpActionResult Get(int id)
        {
            var p = _srv.Get(id);
            if (p == null) return NotFound();
            return Ok(p);
        }

        // POST api/pedidos-proveedor
        [HttpPost, Route("")]
        public IHttpActionResult Create(PedidoProveedor pedido)
        {
            var created = _srv.Create(pedido);
            return Created("", created);
        }

        // PUT api/pedidos-proveedor/5
        [HttpPut, Route("{id:int}")]
        public IHttpActionResult Update(int id, PedidoProveedor pedido)
        {
            if (id != pedido.id_pedido) return BadRequest();
            _srv.Update(pedido);
            return StatusCode(HttpStatusCode.NoContent);
        }

        // DELETE api/pedidos-proveedor/5
        [HttpDelete, Route("{id:int}")]
        public IHttpActionResult Delete(int id)
        {
            if (!_srv.Delete(id)) return NotFound();
            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}
