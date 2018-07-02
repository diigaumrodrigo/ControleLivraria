using System;
using System.IO;
using System.Web;

namespace Repositorio.Negocio.Util
{
    public static class Log
    {
        private static string diretorioLog
        {
            get
            {
                if (!Directory.Exists(Path.Combine(HttpRuntime.AppDomainAppPath, "logFiles")))
                    Directory.CreateDirectory(Path.Combine(HttpRuntime.AppDomainAppPath, "logFiles"));

                return Path.Combine(HttpRuntime.AppDomainAppPath, "logFiles");
            }
        }

        public static void RegistraTxt(string erro, Exception ex)
        {
            var data = DateTime.Now.ToString("yyyyMMdd HHmmss");
            using (StreamWriter str = new StreamWriter(Path.Combine(diretorioLog, "Erro " + data + ".txt")))
            {
                str.WriteLine($"Observacao: {erro} | {GetException(ex)}");
                str.Flush();
            }
        }

        private static string GetException(Exception ex)
        {
            if (ex == null)
                return string.Empty;

            string mensagem = ex?.Message ?? string.Empty;
            string innerException = ex?.InnerException?.Message ?? string.Empty;
            string stacktrace = ex?.StackTrace?.ToString() ?? string.Empty;

            return $"Exception: {mensagem} | InnerException: {innerException} | StackTrace: {stacktrace}";
        }
    }
}
