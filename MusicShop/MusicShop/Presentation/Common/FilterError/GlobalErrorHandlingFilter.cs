using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using MusicShop.Application.Common.Errors;
using NLog;
using System.Data;
using System.Net;

namespace MusicShop.Presentation.Common.FilterError
{
    public class GlobalErrorHandlingFilter : ExceptionFilterAttribute
    {
        private readonly Logger _logger = LogManager.GetLogger("LoggerRule");
        public override void OnException(ExceptionContext context)
        {
            var exception = context.Exception;
            System.Diagnostics.StackTrace trace = new System.Diagnostics.StackTrace(exception, true);
            ProblemDetails problemDetails = new ProblemDetails()
            {
                Title = $"API error : {exception.Message}",
                Status = (int)HttpStatusCode.InternalServerError,
                Detail = $"{exception.InnerException.Message}"

            };

            
            _logger.Error($"Catch exception:{problemDetails.Title}|| trace:{trace.GetFrame(0).GetFileLineNumber()}");
            context.Result = new ObjectResult(problemDetails);
            context.ExceptionHandled = true;
        }
    }
}
