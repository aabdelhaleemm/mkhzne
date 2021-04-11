using System;
using System.Collections.Generic;
using Application.Common.Exceptions;
using Domain.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace mkhzne.Filters
{
    public class ExceptionFilter : ExceptionFilterAttribute
    {
        private readonly Dictionary<Type, Action<ExceptionContext>> _dictionary;

        public ExceptionFilter()
        {
            _dictionary = new Dictionary<Type, Action<ExceptionContext>>
            {
                {typeof(InvalidStockException), InvalidStockExceptionHandler},
                {typeof(CantAddEntityException), CantAddEntityExceptionHandler},
                {typeof(NotFoundException), NotFoundExceptionHandler},
                {typeof(UnAuthorizedRequest),UnAuthorizedRequestHandler}
            };
        }

        public override void OnException(ExceptionContext context)
        {
            var exception = context.Exception.GetType();
            if (_dictionary.ContainsKey(exception))
            {
                _dictionary[exception].Invoke(context);
                return;
            }

            if (!context.ModelState.IsValid)
            {
                InvalidModelStateExceptionHandler(context);
            }

            // UnknownExceptionHandler(context);
        }

        public void UnAuthorizedRequestHandler(ExceptionContext context)
        {
            context.Result = new UnauthorizedObjectResult(context.Exception.Message);
            context.ExceptionHandled = true;
        }

        public void NotFoundExceptionHandler(ExceptionContext context)
        {
            context.Result = new NotFoundObjectResult(context.Exception.Message);
            context.ExceptionHandled = true;
        }

        public void CantAddEntityExceptionHandler(ExceptionContext context)
        {
            var details = new ProblemDetails
            {
                Title = "Cannot add this entity please check the inputs and try again",
                Detail = context.Exception.Message
            };
            context.Result = new BadRequestObjectResult(details);
            context.ExceptionHandled = true;
        }


        private void InvalidStockExceptionHandler(ExceptionContext context)
        {
            var details = new ProblemDetails
            {
                Title = "Invalid stock number!",
                Detail = context.Exception.Message
            };
            context.Result = new BadRequestObjectResult(details);

            context.ExceptionHandled = true;
        }

        private void InvalidModelStateExceptionHandler(ExceptionContext context)
        {
            context.Result = new BadRequestObjectResult(context.ModelState);
            context.ExceptionHandled = true;
        }

        private void UnknownExceptionHandler(ExceptionContext context)
        {
            var details = new ProblemDetails
            {
                Status = StatusCodes.Status500InternalServerError,
                Title = "An error occurred while processing your request."
            };

            context.Result = new ObjectResult(details)
            {
                StatusCode = StatusCodes.Status500InternalServerError
            };

            context.ExceptionHandled = true;
        }
    }
}