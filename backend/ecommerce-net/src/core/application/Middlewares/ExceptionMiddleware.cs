using System.Net;
using application.Errors;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SendGrid.Helpers.Errors.Model;

namespace application.Middlewares;

public class ExceptionMiddleware
{
    private readonly RequestDelegate next;
    private readonly ILogger<ExceptionMiddleware> logger;

    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
    {
        this.next = next;
        this.logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, ex.Message);
            context.Response.ContentType = "application/json";
            var statusCode = (int)HttpStatusCode.InternalServerError;
            var result = string.Empty;

            switch (ex)
            {
                case NotFoundException notFound:
                    statusCode = (int)HttpStatusCode.NotFound;
                    break;

                case ValidationException validationException:
                    statusCode = (int)HttpStatusCode.BadRequest;
                    var errors = validationException.Errors.Select(ers => ers.ErrorMessage).ToArray();
                    var validationJsons = JsonConvert.SerializeObject(errors);

                    result = JsonConvert.SerializeObject(
                        new CodeErrorException(statusCode, errors, validationJsons)
                    );

                    break;
                case BadRequestException badRequestException:
                    statusCode = (int)HttpStatusCode.BadRequest;
                    break;

                default:
                    statusCode = (int)HttpStatusCode.InternalServerError;
                    break;
            }

            if (string.IsNullOrEmpty(result))
            {
                result = JsonConvert.SerializeObject(
                    new CodeErrorException(statusCode,
                    new string[] { ex.Message }, ex.StackTrace));
            }

            context.Response.StatusCode = statusCode;

            await context.Response.WriteAsync(result);


        }
    }
}
