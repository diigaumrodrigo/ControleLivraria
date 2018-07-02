using Repositorio.Negocio.Exceptions;
using Repositorio.Negocio.Util;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;

namespace Repositorio.Aplicacao.Filter
{
    [AttributeUsage(AttributeTargets.All)]
    public sealed class ExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            Exception exception = actionExecutedContext.Exception;

            RegistraLog(exception);

            if (exception is ValidacaoException)
                actionExecutedContext.Response = CreateBadRequestResponse(actionExecutedContext, GetMessagens(exception));
            else
                actionExecutedContext.Response = CreateErrorResponse(actionExecutedContext, GetMessagens(exception));
        }

        private static IEnumerable<string> GetMessagens(Exception exception)
        {
            if (exception is ValidacaoException)
            {
                var validacao = exception as ValidacaoException;
                return validacao.Mensagens;
            }
            else
                return new string[] { "Ocorreu um erro. Por favor, tente novamente." };
        }

        private static void RegistraLog(Exception exception)
        {
            if (exception is ValidacaoException)
            {
                var validacao = exception as ValidacaoException;
                foreach (var mensagem in validacao.Mensagens)
                    Log.RegistraTxt(mensagem, new Exception("Problema Web Api"));
            }
            else
                Log.RegistraTxt(string.Empty, exception);
        }

        private static HttpResponseMessage CreateErrorResponse(HttpActionExecutedContext actionExecutedContext, IEnumerable<string> mensagens)
        {
            return CreateResponse(actionExecutedContext, HttpStatusCode.InternalServerError, mensagens);
        }

        private static HttpResponseMessage CreateBadRequestResponse(HttpActionExecutedContext actionExecutedContext, IEnumerable<string> mensagens)
        {
            return CreateResponse(actionExecutedContext, HttpStatusCode.BadRequest, mensagens);
        }

        private static HttpResponseMessage CreateResponse(HttpActionExecutedContext actionExecutedContext, HttpStatusCode codigo, IEnumerable<string> mensagens)
        {
            return actionExecutedContext.Request.CreateResponse(codigo, mensagens);
        }
    }
}