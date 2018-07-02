using Repositorio.Aplicacao.Filter;
using Repositorio.Aplicacao.Models;
using Repositorio.Aplicacao.Validate;
using System.Web.Http;

namespace Repositorio.Aplicacao
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.EnableCors();

            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "With_Controller-Action-Id",
                routeTemplate: "api/{controller}/{action}/{id}"
            );

            config.Routes.MapHttpRoute(
                name: "With_Controller-Action",
                routeTemplate: "api/{controller}/{action}"
            );

            config.Filters.Add(new ExceptionFilter());
            config.Filters.Add(new ValidateModelAttribute());

            config.DependencyResolver = new UnityConfig().ResolverTipos();
        }
    }
}
