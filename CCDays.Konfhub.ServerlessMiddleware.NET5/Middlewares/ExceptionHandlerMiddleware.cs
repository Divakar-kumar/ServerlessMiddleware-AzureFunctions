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
    /// Middleware for demonstrating ExceptionHandler
    /// </summary>
    public class ExceptionHandlerMiddleware
    {
        /// <summary>
        /// Invoke ExceptionHandler middleware 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="next"></param>
        /// <returns></returns>
        public async Task Invoke(FunctionExecutionContext context, FunctionExecutionDelegate next)
        {
            context.Logger?.Log(LogLevel.Information, "Inside ExceptionHandler middleware");

            context.InvocationResult = new HttpResponseData(HttpStatusCode.Unauthorized);

            var response = context.InvocationResult as HttpResponseData;

            response.Body += "\n Request stage - ExceptionHandler middleware";

            try
            {
                await next.Invoke(context);
            }
            catch(Exception ex)
            {
                context.Logger?.Log(LogLevel.Error, "Catched unhandled exceptions: "+ex.Message);
                
                await Task.CompletedTask;
            }

            ResponseStage(context);
        }
        /// <summary>
        /// Pass through respose stage of ExceptionHandler middleware
        /// </summary>
        /// <param name="context"></param>
        private void ResponseStage(FunctionExecutionContext context)
        {
            var response = context.InvocationResult as HttpResponseData;

            response.Body += "\n Response stage - ExceptionHandler midddleware!";

            context.Logger?.Log(LogLevel.Information, "outside ExceptionHandler middleware");

        }
    }
}
