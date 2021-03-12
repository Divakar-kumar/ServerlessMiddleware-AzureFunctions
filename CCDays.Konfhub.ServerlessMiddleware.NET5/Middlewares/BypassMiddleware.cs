using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Pipeline;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CCDays.Konfhub.ServerlessMiddleware.NET5.Middlewares
{
    /// <summary>
    /// Middleware for demonstrating generate response/short-circuit scenario
    /// </summary>
    public class BypassMiddleware
    {
        /// <summary>
        /// Invoke Bypass middleware 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="next"></param>
        /// <returns></returns>
        public async Task Invoke(FunctionExecutionContext context, FunctionExecutionDelegate next)
        {
            context.Logger?.Log(LogLevel.Information, "Inside Bypass middleware");

            context.InvocationResult = new HttpResponseData(HttpStatusCode.Unauthorized);

            var response = context.InvocationResult as HttpResponseData;

            response.Body += "\n Request stage - Bypass middleware";

            await Task.FromResult(Task.CompletedTask);

            ResponseStage(context);
        }
        /// <summary>
        /// Pass through respose stage of Bypass middleware
        /// </summary>
        /// <param name="context"></param>
        private void ResponseStage(FunctionExecutionContext context)
        {
            var response = context.InvocationResult as HttpResponseData;

            response.Body += "\n Response stage - Bypass midddleware!";

            context.Logger?.Log(LogLevel.Information, "outside Bypass middleware");

        }
    }
}
