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
                Status = (int)HttpStatusCode.InternalServerError,

            };
            //various exceptions
            if (exception.InnerException is SqlException sqlException)
            {
                switch (sqlException.Number)
                {
                    case 2601:
                        problemDetails.Title = "Duplicate name.";
                        break;
                }
            }
            if(exception is DuplicateEmailError)
            {
                problemDetails.Title = "Email already exist.";
                problemDetails.Status = StatusCodes.Status409Conflict;
            }
            if(exception is InvalidEmail)
            {
                problemDetails.Title = "Email is incorrect or does not exist.";
            }
            if(exception is InvalidPassword)
            {
                problemDetails.Title = "Password is incorrect or does not exist.";
            }

            else
            {
                problemDetails.Title = exception.Message;
            }
            _logger.Error($"Catch exception:{problemDetails.Title}|| trace:{trace.GetFrame(0).GetFileLineNumber()}");
            context.Result = new ObjectResult(problemDetails);
            context.ExceptionHandled = true;
        }
    }
}
