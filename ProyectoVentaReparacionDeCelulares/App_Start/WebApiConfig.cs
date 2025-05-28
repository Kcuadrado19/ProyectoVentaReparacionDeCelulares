using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using ProyectoVentaReparacionDeCelulares.Clases;

namespace ProyectoVentaReparacionDeCelulares
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Configuración y servicios de Web API

            config.MessageHandlers.Add(new TokenValidationHandler());


            // Rutas de Web API
            config.MapHttpAttributeRoutes();
          

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.Always;
            // *********** NUEVO BLOQUE ***********
            var json = config.Formatters.JsonFormatter.SerializerSettings;
            // 1) Ignorar propiedades null
            json.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
            // 2) Evitar bucles de referencias (Entity Framework)
            json.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            // 3) (Opcional) Sacar todos los nombres en camelCase
            json.ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver();
            // ***********************************
        }
    }
}
