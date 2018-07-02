using Repositorio.DAL.Contexto;
using Repositorio.DAL.Repositorios;
using Repositorio.Entidades.Entidades;
using Repositorio.Negocio.Exceptions;
using Repositorio.Negocio.Models.Request;
using Repositorio.Negocio.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Repositorio.Negocio.Models
{
    public class Livros : ILivros
    {
        private LivrosRepositorio repLivros { get; set; }
        private CategoriaRepositorio repCategoria { get; set; }

        public Livros(LivrosRepositorio repLivros, CategoriaRepositorio repCategoria)
        {
            this.repLivros = repLivros;
            this.repCategoria = repCategoria;
        }

        public IEnumerable<LivrosReponseModel> GetLivros()
        {
            return repLivros
                .GetAll()
                .Where(livro => livro.Ativo)
                .OrderBy(livro => livro.Titulo)
                .Select(livro => new LivrosReponseModel()
                {
                    IdLivro = livro.IdLivro,
                    Titulo = livro.Titulo,
                    Autor = livro.Autor,
                    IdCategoria = livro.IdCategoria,
                    Categoria = livro.TB_Categoria.Descricao,
                    Ano = livro.Ano,
                    DataCadastro = livro.DataCadastro
                }).ToList();
        }

        public void Adicionar(AdicionarLivroRequestModel request)
        {
            if (LivroJaExiste(request.Titulo, request.Autor))
                throw new ValidacaoException("Dados informados (Titulo e Autor) já existem no banco de dados");

            repLivros.Add(new TB_Livros()
            {
                Titulo = request.Titulo,
                Autor = request.Autor,
                Ano = request.Ano.Value,
                IdCategoria = request.Categoria.Value
            });

            repLivros.SaveAllChanges();
        }

        public void Editar(EditarLivroRequestModel request)
        {
            var livroBD = GetLivro(request.IdLivro);

            livroBD.Titulo = request.Titulo;
            livroBD.Autor = request.Autor;
            livroBD.Ano = request.Ano.Value;
            livroBD.IdCategoria = request.Categoria.Value;

            repLivros.SaveAllChanges();
        }

        public void Deletar(int idLivro)
        {
            repLivros.Delete(livro => livro.IdLivro == idLivro);
            repLivros.SaveAllChanges();
        }

        private TB_Livros GetLivro(int idLivro)
        {
            return repLivros.GetAll().Where(livro => livro.IdLivro == idLivro).First();
        }

        public void InserirBaseLivros()
        {
            using (var trans = repLivros.BeginTransaction())
            {
                try
                {
                    LimparLivros();

                    var categoria = new Categorias(repLivros.ctx as LivrariaContexto);
                    categoria.InserirBaseCategorias();

                    var categorias = repCategoria.GetAll().ToList();

                    var livros = new List<TB_Livros>()
                    {
                        new TB_Livros()
                        {
                            Titulo = "Dom Casmurro",
                            Autor = "Machado de Assis",
                            Ano = 1899,
                            IdCategoria = categorias.First(t => t.Descricao == "Literatura Brasileira").IdCategoria
                        },
                        new TB_Livros()
                        {
                            Titulo = "Iracema",
                            Autor = "José de Alencar",
                            Ano = 1865,
                            IdCategoria = categorias.First(t => t.Descricao == "Literatura Brasileira").IdCategoria
                        },
                        new TB_Livros()
                        {
                            Titulo = "Orgulho e preconceito",
                            Autor = "Jane Austen",
                            Ano = 1813,
                            IdCategoria = categorias.First(t => t.Descricao == "Romance").IdCategoria
                        },
                        new TB_Livros()
                        {
                            Titulo = "Cinderela",
                            Autor = "Irmãos Grim",
                            Ano = 1812,
                            IdCategoria = categorias.First(t => t.Descricao == "Contos").IdCategoria
                        }
                    };

                    foreach (var livro in livros)
                    {
                        if (!LivroJaExiste(livro.Titulo, livro.Autor))
                            repLivros.Add(livro);
                    }

                    repLivros.SaveAllChanges();
                    trans.Commit();
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    throw ex;
                }
            }
        }

        private void LimparLivros()
        {
            repLivros.Delete(livro => livro.IdLivro > 0);
            repLivros.SaveAllChanges();
        }

        private bool LivroJaExiste(string titulo, string autor)
        {
            return repLivros
                .GetAll()
                .Where(livro => livro.Titulo == titulo && livro.Autor == autor)
                .Count() > 0;
        }
    }
}
