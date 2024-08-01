using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using ServiceLayer.CustomException;
using ServiceLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Filter
{
    public class CustomExceptionFilter : IExceptionFilter
    {
        private readonly ILogger<CustomExceptionFilter> _logger;

        public CustomExceptionFilter(ILogger<CustomExceptionFilter> logger)
        {
            _logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            var ex = context.Exception;
            _logger.LogError(ex, ex.Message);

            var error = new ErrorDTO
            {
                Timestamp = DateTime.UtcNow,
                Status = 400,
                Path = context.HttpContext.Request.Path
            };

            if (ex is InvalidModelStateException modelState)
            {
                var fieldErrors = modelState.ModelState
                    .SelectMany(ms => ms.Value.Errors.Select(e => new { ms.Key, e.ErrorMessage }))
                    .ToList();

                foreach (var fieldError in fieldErrors)
                {
                    error.AddError(fieldError.Key, fieldError.ErrorMessage);
                }
            }
            else
            {
                error.AddError("General", ex.Message);
            }

            context.Result = new ObjectResult(error)
            {
                StatusCode = 400 // or use (int)HttpStatusCode.BadRequest
            };

            context.ExceptionHandled = true;
        }
    }
}