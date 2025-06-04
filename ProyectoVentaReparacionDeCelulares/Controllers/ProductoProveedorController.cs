using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using ProyectoVentaReparacionDeCelulares.Interface;
using ProyectoVentaReparacionDeCelulares.Models;
using ProyectoVentaReparacionDeCelulares.Services;

namespace ProyectoVentaReparacionDeCelulares.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("api/proveedores/{provId:int}/productos")]
    public class ProductoProveedorController : ApiController
    {
        private readonly IProductoProveedorService _srv = new ProductoProveedorService();

      
        [HttpGet, Route("")]
        public IEnumerable<ProductoProveedor> Get(int provId) =>
            _srv.GetByProveedor(provId);

        
        [HttpGet, Route("{id:int}")]
        public IHttpActionResult GetById(int provId, int id)
        {
            var p = _srv.GetByProveedor(provId)
                        .FirstOrDefault(x => x.id_producto_proveedor == id);
            if (p == null) return NotFound();
            return Ok(p);
        }

        
        [HttpPost, Route("")]
        public IHttpActionResult Create(int provId, ProductoProveedor p)
        {
            p.id_usuario_proveedor = provId;
            var created = _srv.Create(p);
            return Created("", created);
        }


        [HttpGet]
        [Route("~/api/productos-proveedor")]
        public IEnumerable<ProductoProveedor> GetAll()
        {
            return _srv.GetAll();
        }




        [HttpPut, Route("{id:int}")]
       public IHttpActionResult Update(int provId, int id, ProductoProveedor p)
        {
           if (id != p.id_producto_proveedor || provId != p.id_usuario_proveedor)
               return BadRequest();

           _srv.Update(p);
          return Ok(new { message = "Producto actualizado con éxito" });
        }




        [HttpDelete, Route("{id:int}")]
       public IHttpActionResult Delete(int provId, int id)
       {
            if (!_srv.Delete(id))
               return NotFound();

           return Ok(new { message = "Producto eliminado correctamente" });
       }
    }
}