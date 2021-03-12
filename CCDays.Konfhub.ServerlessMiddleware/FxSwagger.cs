using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using AzureFunctions.Extensions.Swashbuckle.Attribute;
using AzureFunctions.Extensions.Swashbuckle;
using System.Net.Http;

namespace CCDays.Konfhub.ServerlessMiddleware
{
    /// <summary>
    /// Swagger Functions
    /// </summary>
    public static class FxSwagger
    {
        /// <summary>
        /// Swagger documention endpoint - JSON format
        /// </summary>
        /// <param name="req"></param>
        /// <param name="log"></param>
        /// <param name="swashBuckleClient"></param>
        /// <returns></returns>
        [SwaggerIgnore]
        [FunctionName("SwaggerJSON")]
        public static async Task<HttpResponseMessage> SwaggerJSON(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "swagger/json")] HttpRequestMessage req,
            ILogger log,[SwashBuckleClient]ISwashBuckleClient swashBuckleClient)
        {
            return await Task.FromResult(swashBuckleClient.CreateSwaggerDocumentResponse(req));
        }
        /// <summary>
        /// Swagger documentation endpoint - HTML format
        /// </summary>
        /// <param name="req"></param>
        /// <param name="log"></param>
        /// <param name="swashBuckleClient"></param>
        /// <returns></returns>
        [SwaggerIgnore]
        [FunctionName("SwaggerUI")]
        public static async Task<HttpResponseMessage> SwaggerUI(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "swagger/ui")] HttpRequestMessage req,
            ILogger log, [SwashBuckleClient] ISwashBuckleClient swashBuckleClient)
        {
            return await Task.FromResult(swashBuckleClient.CreateSwaggerUIResponse(req,"swagger/json"));
        }
    }
}
