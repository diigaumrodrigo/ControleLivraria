using Repositorio.DAL.Conexao;
using Repositorio.Entidades.Entidades;

namespace Repositorio.DAL.Repositorios
{
    public class CategoriaRepositorio : Base.Repositorio<TB_Categoria>
    {
        public CategoriaRepositorio(IProvedorBanco provedorBanco)
            : base(provedorBanco.Livraria)
        { }
    }
}
