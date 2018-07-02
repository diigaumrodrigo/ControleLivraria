using Repositorio.DAL.Conexao;
using Repositorio.Entidades.Entidades;
using Repositorio.Entidades.Entidades.Configuracoes;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.SQLite;

namespace Repositorio.DAL.Contexto
{
    public class LivrariaContexto : DbContext
    {
        public LivrariaContexto(IProvedorConexao provedorConexao)
            : base(new SQLiteConnection()
            {
                ConnectionString = provedorConexao.ConnectionString().ConnectionString
            }, true)
        {
            this.Database.Initialize(true);
            this.Configuration.LazyLoadingEnabled = false;
            this.Configuration.ProxyCreationEnabled = false;
        }

        public DbSet<TB_Categoria> TB_Categoria { get; set; }
        public DbSet<TB_Livros> TB_Livros { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            Database.SetInitializer(new SQLite.CodeFirst.SqliteCreateDatabaseIfNotExists<LivrariaContexto>(modelBuilder));

            modelBuilder.Configurations.Add(new TB_Categoria_Configuracao());
            modelBuilder.Configurations.Add(new TB_Livros_Configuracao());
        }
    }
}
