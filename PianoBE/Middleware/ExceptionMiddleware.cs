using Newtonsoft.Json;
using ServiceLayer.CustomException;
using ServiceLayer.ModelViews.Enums;

namespace API.Middleware;
public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (CoreException ex)
        {
            await HandleExceptionAsync(httpContext, ex);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(httpContext, new CoreException(StatusCodes.Status500InternalServerError.ToString(), ex.ToString()));
        }
    }

    private Task HandleExceptionAsync(HttpContext context, CoreException exception)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)exception.StatusCode;

        var response = new
        {
            error = exception.Message,
            statusCode = (int)exception.StatusCode
        };

        return context.Response.WriteAsync(JsonConvert.SerializeObject(response));
    }
}