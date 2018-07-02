using Repositorio.DAL.Contexto;
using System.Data.Entity;
using System;

namespace Repositorio.DAL.Conexao
{
    public class ProvedorBanco : IProvedorBanco
    {
        private readonly IProvedorConexao provedorConexao;

        public ProvedorBanco(IProvedorConexao provedorConexao)
        {
            this.provedorConexao = provedorConexao;
        }

        DbContext IProvedorBanco.Livraria => new LivrariaContexto(provedorConexao);
    }
}
