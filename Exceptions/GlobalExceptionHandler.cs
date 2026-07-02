using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace EmbarcaPro.API.Exceptions
{
    public class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger) : IExceptionHandler
    {

        public async ValueTask<bool> TryHandleAsync(
            HttpContext context,
            Exception exception,
            CancellationToken cancellationToken)
        {
            logger.LogError(exception, "Uma exceção não tratada ocorreu: {Message}", exception.Message);


            var statusCode = exception switch
            {
                ArgumentException => StatusCodes.Status400BadRequest,
                KeyNotFoundException => StatusCodes.Status404NotFound,
                // Qualquer outro erro vira 500 (Erro interno)
                _ => StatusCodes.Status500InternalServerError,
            };

            var title = statusCode switch
            {
                StatusCodes.Status400BadRequest => "Requisição inválida.",
                StatusCodes.Status404NotFound => "Recurso não encontrado",
                _ => "Erro interno no servidor."
            };

            var detail = statusCode switch
            {
                StatusCodes.Status500InternalServerError => "Ocorreu um erro inesperado. Tente novamente mais tarde.",
                _ => exception.Message
            };

            var problemDetails = new ProblemDetails
            {
                Status = statusCode,
                Title = title,
                Detail = detail,
                Instance = context.Request.Path
            };

            context.Response.StatusCode = problemDetails.Status.Value;
            context.Response.ContentType = "application/problem+json";

            await context.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

            return true;

        }

    }
}
