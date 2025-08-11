using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using Furniture.Application.Dtos;
using Furniture.Application.Exceptions;
using FluentValidation;
using FluentValidation.Results;
using ValidationException = FluentValidation.ValidationException;

namespace Furniture.WebApi.Middleware;

public class GlobalExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<GlobalExceptionHandlingMiddleware> _logger;

    public GlobalExceptionHandlingMiddleware(
        RequestDelegate next,
        ILogger<GlobalExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unhandled exception occurred: {Message}", ex.Message);
            await HandleExceptionAsync(context, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var response = context.Response;
        response.ContentType = "application/json";

        switch (exception)
        {
            case ValidationException validationEx:
                response.StatusCode = 400;
                await response.WriteAsync(JsonSerializer.Serialize(new ErrorResponseDto
                {
                    StatusCode = 400,
                    Message = "Validation failed.",
                    Errors = validationEx.Errors.Select(e => e.ErrorMessage).ToList()
                }, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase }));
                break;

            case NotFoundException:
                response.StatusCode = 404;
                await response.WriteAsync(JsonSerializer.Serialize(new ErrorResponseDto
                {
                    StatusCode = 404,
                    Message = "Resource not found."
                }, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase }));
                break;

            default:
                response.StatusCode = 500;
                await response.WriteAsync(JsonSerializer.Serialize(ErrorResponseDto.FromException(exception, 500),
                    new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase }));
                break;
        }
    }
}