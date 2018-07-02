using System.ComponentModel.DataAnnotations;

namespace Repositorio.Negocio.Models.Request
{
    public class AdicionarLivroRequestModel
    {
        [Required(ErrorMessage = "Título do livro é obrigatório")]
        public string Titulo { get; set; }

        [Required(ErrorMessage = "Autor do livro é obrigatório")]
        public string Autor { get; set; }

        [Required(ErrorMessage = "Id da categoria é obrigatório")]
        public int? Categoria { get; set; }

        [Required(ErrorMessage = "Ano do livro é obrigatório")]
        public int? Ano { get; set; }
    }
}
