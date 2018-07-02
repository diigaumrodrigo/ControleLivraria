using System;
using System.Collections.Generic;

namespace Repositorio.Entidades.Entidades
{
    public class TB_Categoria
    {
        public int IdCategoria { get; set; }
        public string Descricao { get; set; }
        public bool Ativo { get; set; } = true;
        public DateTime DataCadastro { get; set; } = DateTime.Now;

        public ICollection<TB_Livros> TB_Livros { get; set; } = new HashSet<TB_Livros>();
    }
}
