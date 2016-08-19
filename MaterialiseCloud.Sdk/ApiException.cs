using System;
using System.Net;

namespace MaterialiseCloud.Sdk
{
    public class ApiClientException : Exception
    {
        public ApiClientException(HttpStatusCode statusCode, ErrorResponse response)
        {
            StatusCode = statusCode;
            Response = response;
        }

        public HttpStatusCode StatusCode { get; private set; }
        public ErrorResponse Response { get; private set; }
    }
}