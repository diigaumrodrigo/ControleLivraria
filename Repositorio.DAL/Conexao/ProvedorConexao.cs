using System.Data.SQLite;
using System.IO;
using System.Web;

namespace Repositorio.DAL.Conexao
{
    public class ProvedorConexao : IProvedorConexao
    {
        private string DiretorioBase { get { return HttpRuntime.AppDomainAppPath; } }

        public SQLiteConnectionStringBuilder ConnectionString()
        {
            var pastaBD = Path.Combine(DiretorioBase, "BD");

            if (!Directory.Exists(pastaBD))
                Directory.CreateDirectory(pastaBD);

            var nomeArquivo = Path.Combine(pastaBD, "livraria.db");
            return new SQLiteConnectionStringBuilder()
            {
                DataSource = nomeArquivo,
                ForeignKeys = true
            };
        }
    }
}
