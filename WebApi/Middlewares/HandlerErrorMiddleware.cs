using Newtonsoft.Json;
using System.Net;
using WebApi.Aplicacion.Commons.Exceptions;

namespace WebApi.Middlewares
{
    public class HandlerErrorMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<HandlerErrorMiddleware> _logger;
        public HandlerErrorMiddleware(RequestDelegate next, ILogger<HandlerErrorMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await ManagerExceptionsAsync(context, ex, _logger);
            }
        }


        private async Task ManagerExceptionsAsync(HttpContext context, Exception ex, ILogger<HandlerErrorMiddleware> logger)
        {
            object errores = null;
            switch (ex)
            {
                case CustomException me:
                    logger.LogError(ex, "Middleware Error");
                    errores = me.Errores;
                    context.Response.StatusCode = (int)me.Codigo;
                    break;
                case Exception e:
                    logger.LogError(ex, "Error de SERVIDOR");
                    errores = new { mensaje = string.IsNullOrWhiteSpace(e.Message) ? "Error" : e.Message };
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    break;
            }

            context.Response.ContentType = "application/json";
            string resultados = string.Empty;

            if (errores != null)
            {
                resultados = JsonConvert.SerializeObject(new { errores });
            }

            await context.Response.WriteAsync(resultados);
        }
    }
}
