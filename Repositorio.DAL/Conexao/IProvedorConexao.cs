using System.Data.SQLite;

namespace Repositorio.DAL.Conexao
{
    public interface IProvedorConexao
    {
        SQLiteConnectionStringBuilder ConnectionString();
    }
}
