using Repositorio.DAL.Conexao;
using Repositorio.Entidades.Entidades;

namespace Repositorio.DAL.Repositorios
{
    public sealed class LivrosRepositorio : Base.Repositorio<TB_Livros>
    {
        public LivrosRepositorio(IProvedorBanco provedorBanco)
            : base(provedorBanco.Livraria)
        { }
    }
}
