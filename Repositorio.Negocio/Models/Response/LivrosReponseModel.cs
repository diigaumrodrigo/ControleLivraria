using System;

namespace Repositorio.Negocio.Models.Response
{
    public class LivrosReponseModel
    {
        public int IdLivro { get; set; }
        public string Titulo { get; set; }
        public string Autor { get; set; }
        public int IdCategoria { get; set; }
        public string Categoria { get; set; }
        public int Ano { get; set; }
        public DateTime DataCadastro { get; set; }
    }
}
