using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Pipeline;
using System.Net;
using System.Collections.Generic;
using System.Diagnostics;

namespace CCDays.Konfhub.ServerlessMiddleware.NET5
{
    /// <summary>
    /// Middleware function
    /// </summary>
    public static class FxMiddelware
    {
        /// <summary>
        /// Serverless middleware function in .NET 5 
        /// </summary>
        /// <param name="req"></param>
        /// <param name="executionContext"></param>
        /// <returns></returns>
        [FunctionName("ServerlessMiddleware")]
        public static HttpResponseData ServerlessMiddleware([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "middleware")] HttpRequestData req,
            FunctionExecutionContext executionContext)
        {

            var logger = executionContext.Logger;

            logger.LogInformation("Executed serverless middleware function successfully");

            var response = executionContext.InvocationResult as HttpResponseData;
            
            response.Body += "\n <h1> Welcome to Serverless midlleware in azure functions ! </h1>";

            //throw new Exception("testing exception handler middleware");

            return response;

        }
    }
}
