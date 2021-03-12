using CCDays.Konfhub.MiddlewareLibrary.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CCDays.Konfhub.ServerlessMiddleware.Pipeline
{
    public interface IPipelineFactory
    {
        IPipelineBuilder UseExceptionHandler(Func<HttpContext, Task<IActionResult>> executeFunctions);
    }
}
