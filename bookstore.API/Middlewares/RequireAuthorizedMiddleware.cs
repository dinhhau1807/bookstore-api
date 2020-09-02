using bookstore.Shared.Helpers;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bookstore.API.Middlewares
{
    public class RequireAuthorizedMiddleware
    {
        private readonly RequestDelegate _next;

        public RequireAuthorizedMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            await _next(httpContext);

            if (httpContext.Response.StatusCode == StatusCodes.Status401Unauthorized)
                throw new AppException(httpContext.Response.StatusCode, "You're not logged in! Please log in to get access!");

            if (httpContext.Response.StatusCode == StatusCodes.Status403Forbidden)
                throw new AppException(httpContext.Response.StatusCode, "You're not allowed to perform this action!");
        }
    }
}
