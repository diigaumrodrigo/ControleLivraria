using Repositorio.DAL.Contexto;
using Repositorio.Entidades.Entidades;
using Repositorio.Negocio.Models.Response;
using System.Collections.Generic;
using System.Linq;

namespace Repositorio.Negocio.Models
{
    public class Categorias
    {
        private LivrariaContexto ctx { get; set; }

        public Categorias(LivrariaContexto ctx)
        {
            this.ctx = ctx;
        }

        public IEnumerable<CategoriaResponseModel> GetCategorias()
        {
            return ctx.Set<TB_Categoria>()
                .Where(categoria => categoria.Ativo)
                .Select(categoria => new CategoriaResponseModel()
                {
                    IdCategoria = categoria.IdCategoria,
                    Descricao = categoria.Descricao
                }).ToList();
        }

        public void InserirBaseCategorias()
        {
            var categorias = new List<TB_Categoria>()
            {
                new TB_Categoria() { Descricao = "Literatura Brasileira" },
                new TB_Categoria() { Descricao = "Romance" },
                new TB_Categoria() { Descricao = "Contos" }
            };

            LimparCategorias();

            foreach (var categoria in categorias)
            {
                if (!CategoriaJaExiste(categoria.Descricao))
                    ctx.TB_Categoria.Add(categoria);
            }

            ctx.SaveChanges();
        }

        public void LimparCategorias()
        {
            ctx.Set<TB_Categoria>()
                .ToList()
                .ForEach(cat => ctx.Set<TB_Categoria>().Remove(cat));

            ctx.SaveChanges();
        }        

        private bool CategoriaJaExiste(string categoria)
        {
            return ctx.Set<TB_Categoria>()
                .Where(cat => cat.Descricao == categoria)
                .Count() > 0;
        }
    }
}
