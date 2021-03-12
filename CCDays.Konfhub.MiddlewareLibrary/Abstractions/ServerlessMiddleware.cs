using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CCDays.Konfhub.MiddlewareLibrary.Abstractions
{
    /// <summary>
    /// Serverless middleware
    /// be the base class
    /// </summary>
    public abstract class ServerlessMiddleware
    {
        public ServerlessMiddleware Next { get; set; }
        protected ServerlessMiddleware()
        {

        }
        protected ServerlessMiddleware(ServerlessMiddleware next)
        {
            this.Next = next;
        }
        public abstract Task InvokeAsync(HttpContext context);

    }
}
