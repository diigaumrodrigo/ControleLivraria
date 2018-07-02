using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;

namespace Repositorio.Entidades.Entidades.Configuracoes
{
    public sealed class TB_Categoria_Configuracao : EntityTypeConfiguration<TB_Categoria>
    {
        public TB_Categoria_Configuracao()
        {
            ToTable("TB_Categoria");

            HasKey(x => x.IdCategoria)
                .Property(x => x.IdCategoria)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
                .IsRequired();

            Property(x => x.Descricao)
                .HasMaxLength(50)
                .HasColumnAnnotation("Index", new IndexAnnotation(new[] {
                    new IndexAttribute("Index") { IsUnique = true }
                })).IsRequired();

            Property(x => x.Ativo).IsRequired();
            Property(x => x.DataCadastro).IsRequired();
        }
    }
}
