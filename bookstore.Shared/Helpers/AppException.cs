using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace bookstore.Shared.Helpers
{
    public class AppException : Exception
    {
        public bool IsOperation { get; } = true;
        public int StatusCode { get; set; } = StatusCodes.Status500InternalServerError;

        public AppException(int statusCode, string message) : base(message)
        {
            StatusCode = statusCode;
        }
    }
}
