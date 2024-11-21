using FluentValidation;
using Student.API.Models;

namespace Student.API.Filters;

internal class ValidationFilter : IEndpointFilter
{
    private readonly IServiceProvider _serviceProvider;

    public ValidationFilter(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async ValueTask<object> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        // Obtém o tipo do objeto a ser validado
        var argument = context.Arguments.FirstOrDefault(arg => arg != null &&
                                                               _serviceProvider.GetService(typeof(IValidator<>).MakeGenericType(arg.GetType())) != null);

        if (argument != null)
        {
            // Recupera o validador do serviço
            var validatorType = typeof(IValidator<>).MakeGenericType(argument.GetType());
            var validator = _serviceProvider.GetService(validatorType) as IValidator;

            if (validator != null)
            {
                // Executa a validação
                var validationResult = await validator.ValidateAsync(new ValidationContext<object>(argument));
                if (!validationResult.IsValid)
                {
                    // Retorna erros em caso de falha
                    var problemDetails = new ValidationProblemDetails
                    {
                        Instance = context.HttpContext.Request.Path,
                        Status = StatusCodes.Status400BadRequest,
                        Title = "Ocorreram um ou mais erros de validação.",
                        Errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList()
                    };

                    return Results.BadRequest(problemDetails);
                }
            }
        }

        // Continua para o próximo filtro ou endpoint
        return await next(context);
    }
}
