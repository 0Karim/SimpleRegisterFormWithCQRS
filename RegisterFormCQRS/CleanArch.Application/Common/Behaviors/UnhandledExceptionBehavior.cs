using CleanArch.Application.Common.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArch.Application.Common.Behaviors
{
    public class UnhandledExceptionBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly ILogger<TRequest> _logger;
        private readonly IDateTime _dateTimeService;

        public UnhandledExceptionBehavior(ILogger<TRequest> logger, IDateTime dateTimeService)
        {
            _logger = logger;
            _dateTimeService = dateTimeService;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            try
            {
                return await next();
            }
            catch (Exception ex)
            {
                var requestName = typeof(TRequest).Name;
                var requestId = typeof(TRequest).GUID;

                if (!ex.GetType().Name.Contains("DbUpdateException"))
                {
                    _logger.LogError(ex, "API_InternalException Unhandled Exception for RequestId = {Id} , Request Name {Name} , {@Request} , {@date} \n {message} \n =========================================="
                        , requestId, requestName, request, _dateTimeService.Now.ToString("dd/MM/yyyy"), ex.Message);
                    throw;
                }
                else
                {
                    throw new Common.Exceptions.DbUpdateException(ex.Message, ex.InnerException);
                }
            }
        }
    }
}
