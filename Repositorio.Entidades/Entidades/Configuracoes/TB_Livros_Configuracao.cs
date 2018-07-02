using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Repositorio.Entidades.Entidades.Configuracoes
{
    public sealed class TB_Livros_Configuracao : EntityTypeConfiguration<TB_Livros>
    {
        public TB_Livros_Configuracao()
        {
            ToTable("TB_Livros");

            HasKey(x => x.IdLivro)
                .Property(x => x.IdLivro)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
                .IsRequired();

            HasRequired(x => x.TB_Categoria)
                .WithMany(x => x.TB_Livros)
                .HasForeignKey(x => x.IdCategoria)
                .WillCascadeOnDelete(false);

            Property(x => x.Titulo).HasMaxLength(100).IsRequired();
            Property(x => x.Autor).HasMaxLength(100).IsRequired();
            Property(x => x.Ano).IsRequired();
            Property(x => x.Ativo).IsRequired();
            Property(x => x.DataCadastro).IsRequired();
        }
    }
}
