using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ProyectoVentaReparacionDeCelulares.Controllers
{

    [RoutePrefix("api/test")]
    public class TestController : ApiController
    {

        [HttpGet]
        [Route("ping")]
        public IHttpActionResult Ping()
        {
            return Ok("pong");
        }


    }
}
