using EmbarcaPro.API.Common.Results;
using EmbarcaPro.API.Dtos.Response;
using Microsoft.AspNetCore.Mvc;

namespace EmbarcaPro.API.Extensions
{
    public static class ServiceResultExtensions
    {
        public static IActionResult ToActionResult<T>(
            this ServiceResult<T> result,
            ControllerBase controller,
            int sucessStatusCode = StatusCodes.Status200OK)
        {
            if (result.Success)
            {
                var response = new ApiResponse<T>(
                    result.Message,
                    result.Data
                    );
                return controller.StatusCode(sucessStatusCode, response);
            }

            var statusCode = result.ErrorType switch
            {
                ErrorType.Validation => StatusCodes.Status400BadRequest,
                ErrorType.Unauthorized => StatusCodes.Status401Unauthorized,
                ErrorType.Forbidden => StatusCodes.Status403Forbidden,
                ErrorType.NotFound => StatusCodes.Status404NotFound,
                ErrorType.Conflict => StatusCodes.Status409Conflict,
                _ => StatusCodes.Status400BadRequest
            };

            var problemDetails = new ProblemDetails
            {
                Status = statusCode,
                Title = GetTitle(result.ErrorType),
                Detail = result.Message
            };

            return controller.StatusCode(statusCode, problemDetails);
        }

        private static string GetTitle(ErrorType errorType)
        {
            return errorType switch
            {

                ErrorType.Validation => "Erro de validação",
                ErrorType.Unauthorized => "Não autorizado",
                ErrorType.Forbidden => "Acesso negado",
                ErrorType.NotFound => "Recurso não encontrado",
                ErrorType.Conflict => "Conflito",
                _ => "Não foi possível processar a solicitação"
            };
        }
    }
}
