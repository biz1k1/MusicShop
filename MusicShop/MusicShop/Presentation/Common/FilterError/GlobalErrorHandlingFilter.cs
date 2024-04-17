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

            };
            //various exceptions
            if (exception.InnerException is SqlException sqlException)
            {
                switch (sqlException.Number)
                {
                    case 2601:
                        problemDetails.Title = $"API error : Duplicate name.";
                        problemDetails.Status= StatusCodes.Status409Conflict;
                        break;
                }
                problemDetails.Detail= exception.InnerException.Message;
            }
            if(exception is DuplicateEmailError)
            {
                problemDetails.Title = $"API error: Email already exist.";
                problemDetails.Status = StatusCodes.Status409Conflict;
            }
            if(exception is InvalidLogin)
            {
                problemDetails.Title = $"API error : Login is incorrect or does not exist.";
                problemDetails.Status = StatusCodes.Status404NotFound;
            }
            if(exception is InvalidPassword)
            {
                problemDetails.Title = $"API error : Password is incorrect or does not exist.";
                problemDetails.Status = StatusCodes.Status404NotFound;
            }
            if(exception is UserNotFound)
            {
                problemDetails.Title = $"API error : User was not found.";
                problemDetails.Status = StatusCodes.Status404NotFound;
            }
            if(exception is CategoryNotFound)
            {
                problemDetails.Title = $"API error : Category was not found.";
                problemDetails.Status = StatusCodes.Status404NotFound;
            }
            if(exception is ProductNotFound)
            {
                problemDetails.Title = $"API error : Product was not found.";
                problemDetails.Status = StatusCodes.Status404NotFound;
            }
            if(exception is CategoryReference)
            {
                problemDetails.Title = $"API error : A category can't refer to itself.";
                problemDetails.Status = StatusCodes.Status400BadRequest;
            }
            

            _logger.Error($"Catch exception:{problemDetails.Title}|| trace:{trace.GetFrame(0).GetFileLineNumber()}");
            context.Result = new ObjectResult(problemDetails);
            context.ExceptionHandled = true;
        }
    }
}
