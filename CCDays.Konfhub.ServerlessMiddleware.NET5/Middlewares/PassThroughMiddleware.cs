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
    /// Middleware to demonstrate pass-through scenario across multiple middlewares
    /// </summary>
    public class PassThroughMiddleware
    {
        /// <summary>
        /// Invoke Pass through middleware
        /// </summary>
        /// <param name="context"></param>
        /// <param name="next"></param>
        /// <returns></returns>
        public async Task Invoke(FunctionExecutionContext context, FunctionExecutionDelegate next)
        {
            context.Logger?.Log(LogLevel.Information, "Inside pass-through middleware");

            context.InvocationResult = new HttpResponseData(HttpStatusCode.OK);

            var response = context.InvocationResult as HttpResponseData;

            response.Body += "\n Request stage - Pass-through middleware";

            await next.Invoke(context);

            ResponseStage(context);
        }
        /// <summary>
        /// Pass through respose stage 
        /// </summary>
        /// <param name="context"></param>
        private void ResponseStage(FunctionExecutionContext context)
        {
            var response = context.InvocationResult as HttpResponseData;

            response.Body += "\n Response stage - Pass-through midddleware!";

            context.Logger?.Log(LogLevel.Information, "outside pass-through middleware");

        }
    }
}
