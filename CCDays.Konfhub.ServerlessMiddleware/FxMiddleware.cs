using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using CCDays.Konfhub.MiddlewareLibrary.Abstractions;
using CCDays.Konfhub.ServerlessMiddleware.Pipeline;
using CCDays.Konfhub.MiddlewareLibrary.Infrastructure;

namespace CCDays.Konfhub.ServerlessMiddleware
{
    public class FxMiddleware
    {
        private readonly IPipelineBuilder pipelineBuilder;

        public FxMiddleware(IPipelineFactory pipelineFactory)
        {
            this.pipelineBuilder = pipelineFactory.UseExceptionHandler(this.ExecuteFunctions);
        }

        [FunctionName("ServerlessMiddleware")]
        public async Task<IActionResult> ServerlessMiddleware(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "middleware")] HttpRequest req,
            ILogger log)
        {
            log.LogDebug("C# HTTP trigger function processed a request.");

            return await this.pipelineBuilder.Execute();
        }
        private async Task<IActionResult> ExecuteFunctions(HttpContext context)
        {
            await HttpResponseWritingExtensions.WriteAsync(context.Response, "\n Hi from serverless middleware functions !");

            throw new Exception("Exception thrown from functions!");
        }
    }
}
