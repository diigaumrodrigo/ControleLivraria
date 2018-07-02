using Repositorio.DAL.Contexto;
using Repositorio.Negocio.Models;
using System.Web.Http;

namespace Repositorio.Aplicacao.Controllers
{
    [RoutePrefix("categorias")]
    public class CategoriaController : ApiController
    {
        private readonly LivrariaContexto ctx;

        public CategoriaController(LivrariaContexto ctx)
        {
            this.ctx = ctx;
        }

        [HttpGet]
        [Route("")]
        public IHttpActionResult Get()
        {
            var categoria = new Categorias(ctx);
            return Ok(categoria.GetCategorias());
        }
    }
}
