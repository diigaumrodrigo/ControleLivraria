using System;
using System.Collections.Generic;

namespace Repositorio.Negocio.Exceptions
{
    [Serializable]
    public class ValidacaoException : Exception
    {
        private List<string> mensagens = new List<string>();

        public ValidacaoException() { }

        public ValidacaoException(IEnumerable<string> mensagens)
        {
            this.mensagens.AddRange(mensagens);
        }

        public ValidacaoException(string message)
            : base(message)
        {
            mensagens.Add(message);
        }

        public ValidacaoException(string message, Exception inner)
            : base(message, inner)
        {
            mensagens.Add(message);
        }

        protected ValidacaoException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context)
        { }

        public override string Message
        {
            get { return string.Empty; }
        }

        public IEnumerable<string> Mensagens
        {
            get { return mensagens.ToArray(); }
        }
    }
}