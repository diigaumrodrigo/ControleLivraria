using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;

namespace Repositorio.Aplicacao.Validate
{
    [AttributeUsage(AttributeTargets.All)]
    public sealed class ValidateModelAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            if (actionContext.ModelState.IsValid)
                return;

            var erros = GetErrosDeValidacao(actionContext);
            actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.BadRequest, erros);
        }

        private static List<string> GetErrosDeValidacao(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            var erros = actionContext.ModelState.Values
                .SelectMany(modelState => modelState.Errors.Select(error => error.ErrorMessage))
                .Where(mensagem => !string.IsNullOrWhiteSpace(mensagem))
                .Distinct()
                .ToList();

            if (erros.Count == 0)
            {
                // Neste momento, existem erros no modelo, mas não foram gerados pelas anotações e sim pelo processo de Model Binding do WebApi
                // portanto apenas a propriedade ErrorMessage da classe ModelError não foi preenchida, somente a propriedade Exception
                erros.Add("Dados inválidos");
            }

            return erros;
        }
    }
}