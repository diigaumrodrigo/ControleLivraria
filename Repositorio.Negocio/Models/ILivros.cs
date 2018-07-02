using Repositorio.Negocio.Models.Request;
using Repositorio.Negocio.Models.Response;
using System.Collections.Generic;

namespace Repositorio.Negocio.Models
{
    public interface ILivros
    {
        IEnumerable<LivrosReponseModel> GetLivros();
        void Adicionar(AdicionarLivroRequestModel request);
        void Editar(EditarLivroRequestModel request);
        void Deletar(int idLivro);
        void InserirBaseLivros();
    }
}
