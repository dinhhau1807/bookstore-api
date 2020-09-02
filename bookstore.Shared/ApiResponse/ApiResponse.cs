using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace bookstore.Shared.ApiResponse
{
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public int StatusCode { get; set; }
        public object Data { get; set; }
        public object ExtraData { get; set; }

        public ApiResponse(T data)
        {
            Success = true;
            StatusCode = StatusCodes.Status200OK;
            Data = data;
        }

        public ApiResponse(int errorCode, object errorData = null)
        {
            Success = false;
            StatusCode = errorCode;
            Data = errorData;
        }

        public static ApiResponse<T> Ok(T data) => new ApiResponse<T>(data);
        public static ApiResponse<T> Error(int errorCode, object errorData = null) => new ApiResponse<T>(errorCode, errorData);
    }
}
