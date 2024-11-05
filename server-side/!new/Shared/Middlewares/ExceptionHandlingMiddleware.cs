using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Shared.Exceptions;
using System.Net;

namespace Shared.Middlewares;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
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
            await HandleException(context, ex);
        }
    }

    private Task HandleException(HttpContext context, Exception ex)
    {
        string error = ex.Message;

        if (ex is RequestFaultException faultEx)
        {
            error = faultEx.Fault.Exceptions.First().Message;
        }

        _logger.LogError($"{error} \n{ex.StackTrace}");

        var code = ex switch
        {
            NotFoundException _ => HttpStatusCode.NotFound,
            BadRequestException _ => HttpStatusCode.BadRequest,
            ForbiddenException _ => HttpStatusCode.Forbidden,
            UnauthorizationException _ => HttpStatusCode.Unauthorized,
            _ => HttpStatusCode.InternalServerError
        };

        context.Response.ContentType = "application/text";
        context.Response.StatusCode = (int)code;

        return context.Response.WriteAsync(error);
    }
}
