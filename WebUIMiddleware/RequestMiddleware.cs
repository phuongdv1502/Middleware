﻿using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebUIMiddleware
{
    public class RequestMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public RequestMiddleware(RequestDelegate next, ILoggerFactory logFactory)
        {
            _next = next;
            _logger = logFactory.CreateLogger("RequestMiddleware");
        }

        public async Task Invoke(HttpContext httpContext)
        {
            _logger.LogInformation($"RequestMiddleware is executing..: query={httpContext.Request.Query["UserName"].ToString()}");
            var queries = Microsoft.AspNetCore.WebUtilities.QueryHelpers.ParseQuery(httpContext.Request.Query["UserName"].ToString());

            foreach (var query in queries)
            {
                httpContext.Request.Headers.Add(query.Key, query.Value);
            }

            await _next(httpContext); // calling next middleware

        }
    }

}
