using System.Data.Entity;

namespace Repositorio.DAL.Conexao
{
    public interface IProvedorBanco
    {
        DbContext Livraria { get; }
    }
}
