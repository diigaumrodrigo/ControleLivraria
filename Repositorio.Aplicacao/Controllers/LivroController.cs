using Repositorio.Negocio.Models;
using Repositorio.Negocio.Models.Request;
using System.Web.Http;

namespace Repositorio.Aplicacao.Controllers
{
    [RoutePrefix("livros")]
    public class LivroController : ApiController
    {
        private readonly ILivros livros;

        public LivroController(ILivros livros)
        {
            this.livros = livros;
        }

        [HttpGet]
        [Route("inserirBaseLivros")]
        public IHttpActionResult InserirBaseLivros()
        {
            livros.InserirBaseLivros();
            return Ok("Base de livros recriada com sucesso");
        }

        [HttpGet]
        [Route("")]
        public IHttpActionResult Get()
        {
            return Ok(livros.GetLivros());
        }

        [HttpPost]
        [Route("adicionar")]
        public IHttpActionResult AdicionarLivro([FromBody] AdicionarLivroRequestModel request)
        {
            livros.Adicionar(request);
            return Ok("Livro inserido com sucesso");
        }

        [HttpPost]
        [Route("editar")]
        public IHttpActionResult EditarLivro([FromBody] EditarLivroRequestModel request)
        {
            livros.Editar(request);
            return Ok("Livro editado com sucesso");
        }

        [HttpPost]
        [Route("deletar/{idLivro:int}")]
        public IHttpActionResult DeletarLivro(int idLivro)
        {
            livros.Deletar(idLivro);
            return Ok("Livro deletado com sucesso");
        }
    }
}
