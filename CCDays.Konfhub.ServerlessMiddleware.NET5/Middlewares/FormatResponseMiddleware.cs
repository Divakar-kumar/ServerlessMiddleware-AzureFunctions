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
    /// Custom middleware to implement data modification on response 
    /// </summary>
    public class FormatResponseMiddleware
    {
        /// <summary>
        /// Invoke format response middleware
        /// </summary>
        /// <param name="context"></param>
        /// <param name="next"></param>
        /// <returns></returns>
        public async Task Invoke(FunctionExecutionContext context, FunctionExecutionDelegate next)
        {
            context.Logger?.Log(LogLevel.Information, "Inside Format response middleware");

            context.InvocationResult = new HttpResponseData(HttpStatusCode.OK);

            var response = context.InvocationResult as HttpResponseData;

            response.Body += "\n Request stage - Format Response middleware!";

            await next.Invoke(context);

            ModifyResponse(context); 
            
            ResponseStage(context);
        }
        /// <summary>
        /// Pass through respose stage of Format Response middleware
        /// </summary>
        /// <param name="context"></param>
        private void ResponseStage(FunctionExecutionContext context)
        {
            var response = context.InvocationResult as HttpResponseData;

            response.Body += "\n Response stage - Format Response midddleware!";

            context.Logger?.Log(LogLevel.Information, "outside Format Response middleware");

        }
        /// <summary>
        /// Modify response
        /// </summary>
        /// <param name="context"></param>
        private void ModifyResponse(FunctionExecutionContext context)
        {
            var response = context.InvocationResult as HttpResponseData;

            response.Body += "\n <h2> Modified Response from a middleware </h2>";

            context.Logger?.Log(LogLevel.Information, "Modified Response from middleware");

        }
    }
}
