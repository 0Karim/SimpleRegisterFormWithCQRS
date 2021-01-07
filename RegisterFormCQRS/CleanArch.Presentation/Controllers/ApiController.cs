using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CleanArch.Application.Common.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArch.Presentation.Controllers
{
    [ApiController]
    [ApplicationExceptionFilter]
    public class ApiController : Controller
    {
        private IMediator _mediator;

        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();
    }


    public class ApplicationExceptionFilter : ExceptionFilterAttribute
    {
        private readonly IDictionary<Type, Action<ExceptionContext>> _exceptionHandlers;

        public ApplicationExceptionFilter()
        {
            // Register known exception types and handlers.
            _exceptionHandlers = new Dictionary<Type, Action<ExceptionContext>>
            {
                { typeof(ValidationException), HandleValidationException },
                { typeof(DbUpdateException), HandleDbUpdateException},
                {typeof(NotFoundException) , HandleNotFoundException }
            };
        }

        private void HandleNotFoundException(ExceptionContext context)
        {
            var exception = context.Exception as NotFoundException;

            var details = new ProblemDetails()
            {
                Type = "https://tools.ietf.org/html/rfc7231#section-6.5.4",
                Title = "The Specified Item Was Not found.",
                Detail = exception.Message
            };

            context.Result = new NotFoundObjectResult(details)
            {
                StatusCode = StatusCodes.Status404NotFound
            };

            context.ExceptionHandled = true;
        }

        public override void OnException(ExceptionContext context)
        {
            HandleException(context);

            base.OnException(context);
        }

        private void HandleException(ExceptionContext context)
        {
            Type type = context.Exception.GetType();
            if (_exceptionHandlers.ContainsKey(type))
            {
                _exceptionHandlers[type].Invoke(context);
                return;
            }

            HandleUnknownException(context);
        }

        private void HandleUnknownException(ExceptionContext context)
        {
            var details = new ProblemDetails
            {
                Status = StatusCodes.Status500InternalServerError,
                Title = "An error occurred while processing your request.",
                Type = "https://tools.ietf.org/html/rfc7231#section-6.6.1",
                Detail = "ExcMsg => " + context.Exception.Message + " InnerExcep => " + context.Exception.InnerException,
            };

            context.Result = new ObjectResult(details)
            {
                StatusCode = StatusCodes.Status500InternalServerError
            };

            context.ExceptionHandled = true;
        }


        private void HandleValidationException(ExceptionContext context)
        {
            var exception = context.Exception as ValidationException;

            var details = new ValidationProblemDetails(exception.Errors)
            {
                Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1"
            };

            context.Result = new BadRequestObjectResult(details)
            {
                StatusCode = StatusCodes.Status400BadRequest
            };

            context.ExceptionHandled = true;
        }

        private void HandleDbUpdateException(ExceptionContext context)
        {
            var deleteMsgException = "The DELETE statement conflicted with the REFERENCE constraint";
            var exception = context.Exception as DbUpdateException;
            var details = new ProblemDetails
            {
                Status = StatusCodes.Status405MethodNotAllowed,
                Title = exception.InnerException.ToString().Contains(deleteMsgException) ? "Cannot Delete this Item" : exception.InnerException.Message
            };
            context.Result = new ObjectResult(details)
            {
                StatusCode = StatusCodes.Status405MethodNotAllowed
            };

            context.ExceptionHandled = true;
        }



    }
}
