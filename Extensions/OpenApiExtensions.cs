using Microsoft.AspNetCore.OpenApi;
using Microsoft.OpenApi;

namespace EmbarcaPro.API.Extensions
{
    /// <summary>
    /// Configuração do documento OpenAPI nativo do ASP.NET Core (substitui o Swashbuckle).
    /// O esquema de segurança JWT precisa ser adicionado via document transformer,
    /// pois o pipeline nativo não tem equivalente ao AddSecurityDefinition.
    /// </summary>
    public static class OpenApiExtensions
    {
        private const string BearerScheme = "Bearer";

        public static IServiceCollection AddEmbarcaProOpenApi(this IServiceCollection services)
        {
            services.AddOpenApi(options =>
            {
                options.AddDocumentTransformer((document, context, cancellationToken) =>
                {
                    document.Info = new OpenApiInfo
                    {
                        Title = "EmbarcaPro.API",
                        Version = "v1",
                        Description = "API do sistema de transporte EmbarcaPro"
                    };

                    document.Components ??= new OpenApiComponents();
                    document.Components.SecuritySchemes ??= new Dictionary<string, IOpenApiSecurityScheme>();

                    document.Components.SecuritySchemes[BearerScheme] = new OpenApiSecurityScheme
                    {
                        Type = SecuritySchemeType.Http,
                        Scheme = "bearer",
                        BearerFormat = "JWT",
                        In = ParameterLocation.Header,
                        Description = "Informe apenas o token, sem o prefixo Bearer."
                    };

                    return Task.CompletedTask;
                });

                // Marca como protegidos apenas os endpoints que exigem autorização,
                // em vez de aplicar o cadeado no documento inteiro.
                options.AddOperationTransformer((operation, context, cancellationToken) =>
                {
                    var requiresAuth = context.Description.ActionDescriptor.EndpointMetadata
                        .OfType<Microsoft.AspNetCore.Authorization.IAuthorizeData>()
                        .Any();

                    if (requiresAuth)
                    {
                        operation.Security =
                        [
                            new OpenApiSecurityRequirement
                            {
                                [new OpenApiSecuritySchemeReference(BearerScheme)] = new List<string>()
                            }
                        ];
                    }

                    return Task.CompletedTask;
                });
            });

            return services;
        }
    }
}