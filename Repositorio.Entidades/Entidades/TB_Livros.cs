using System;

namespace Repositorio.Entidades.Entidades
{
    public class TB_Livros
    {
        public int IdLivro { get; set; }
        public int IdCategoria { get; set; }
        public string Titulo { get; set; }
        public string Autor { get; set; }
        public int Ano { get; set; }
        public bool Ativo { get; set; } = true;
        public DateTime DataCadastro { get; set; } = DateTime.Now;

        public TB_Categoria TB_Categoria { get; set; }
    }
}
