using Repositorio.Negocio.Models;
using System.Web.Http;

namespace Repositorio.Aplicacao
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}
