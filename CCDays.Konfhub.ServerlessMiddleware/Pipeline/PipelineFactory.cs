using CCDays.Konfhub.MiddlewareLibrary.Abstractions;
using CCDays.Konfhub.MiddlewareLibrary.Infrastructure;
using CCDays.Konfhub.MiddlewareLibrary.Middlewares;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CCDays.Konfhub.ServerlessMiddleware.Pipeline
{
    public class PipelineFactory : IPipelineFactory
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public PipelineFactory(IHttpContextAccessor httpContextAccessor)
        {
            this._httpContextAccessor = httpContextAccessor;
        }
        public IPipelineBuilder UseExceptionHandler(Func<HttpContext, Task<IActionResult>> executeFunctions)
        {
            PipelineBuilder builder = new PipelineBuilder(this._httpContextAccessor);

            var middleware = new ExceptionHandlingMiddleware();
            
            return builder.Use(middleware)
                          .Use(new FunctionsMiddleware(executeFunctions));
        }
    }
}
