using COAssistance.API.Util;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json.Converters;
using System.Web.Http;

namespace COAssistance.API
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            // Configure Web API to use only bearer token authentication.
            config.SuppressDefaultHostAuthentication();
            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

            // Web API routes
            config.MapHttpAttributeRoutes();
            config.MessageHandlers.Add(new LanguageMessageHandler());

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Formatters.Remove(config.Formatters.XmlFormatter);

            var jsonFormater = config.Formatters.JsonFormatter;

            jsonFormater.SerializerSettings.Converters.Add(new IsoDateTimeConverter { DateTimeFormat = "yyyy-MM-dd'T'HH:mm:ss" });
            jsonFormater.SupportedMediaTypes.Add(new System.Net.Http.Headers.MediaTypeHeaderValue("multipart/form-data"));

            config.Formatters.Add(jsonFormater);
        }
    }
}