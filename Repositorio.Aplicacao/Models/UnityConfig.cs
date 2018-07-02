using Repositorio.DAL.Conexao;
using Repositorio.Negocio.Models;
using Unity;

namespace Repositorio.Aplicacao.Models
{
    public class UnityConfig
    {
        private IUnityContainer _container;

        public UnityResolver ResolverTipos()
        {
            _container = new UnityContainer();

            _container
                .RegisterType<ILivros, Livros>()
                .RegisterType<IProvedorConexao, ProvedorConexao>()
                .RegisterType<IProvedorBanco, ProvedorBanco>();

            return new UnityResolver(_container);
        }

        public T Prover<T>() where T : class
        {
            return _container.Resolve<T>();
        }
    }
}