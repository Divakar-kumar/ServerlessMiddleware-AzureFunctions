using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CCDays.Konfhub.MiddlewareLibrary.Abstractions
{
    /// <summary>
    /// Provides methods to create middleware pipeline.
    /// </summary>
    public interface IPipelineBuilder
    {        
        /// <summary>
        /// Register middleware to pipeline
        /// </summary>
        /// <param name="middleware"></param>
        /// <returns></returns>
        IPipelineBuilder Use(ServerlessMiddleware middleware);
        /// <summary>
        /// Executes current pipeline
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        Task<IActionResult> Execute();
    }
}
