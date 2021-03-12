using CCDays.Konfhub.MiddlewareLibrary.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CCDays.Konfhub.MiddlewareLibrary.Middlewares
{
    public class FunctionsMiddleware : ServerlessMiddleware
    {
        private readonly Func<HttpContext, Task<IActionResult>> func;

        /// <summary>
        /// Initializes a new instance of the <see cref="FunctionMiddleware"/> class.
        /// </summary>
        /// <param name="func">The task to be executed.</param>
        public FunctionsMiddleware(Func<HttpContext, Task<IActionResult>> func)
        {
            this.func = func;
        }

        /// <summary>
        /// Runs the middleware.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public override async Task InvokeAsync(HttpContext context)
        {
            var result = await this.func(context);

            await Task.CompletedTask;

        }
    }
}
