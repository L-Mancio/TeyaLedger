using System;
using System.Text.Json;
using System.Threading.Tasks;
using Ledger.Domain.BusinessRules;
using Microsoft.AspNetCore.Http;

namespace Ledger.API.Middleware;

public class ExceptionHandlingMiddleware(RequestDelegate next)
{
    private readonly RequestDelegate _next = next;

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (BuisnessRuleException ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = StatusCodes.Status400BadRequest;

        var response = new
        {
            status = 400,
            error = "Business Rule Violation",
            message = exception.Message
        };

        var json = JsonSerializer.Serialize(response);
        await context.Response.WriteAsync(json);
    }
}