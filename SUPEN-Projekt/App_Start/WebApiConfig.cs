using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Web.Http;

namespace SUPEN_Projekt
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();
				config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                
                
                defaults: new { id = RouteParameter.Optional }

                
            );


            config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/octet-stream"));
            var f = new FormUrlEncodedMediaTypeFormatter();
            f.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));
            f.SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/x-www-form-urlencoded"));
        }
    }
}
