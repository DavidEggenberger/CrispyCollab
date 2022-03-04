using System;

namespace WebWasmClient.Services.Http
{
    public class HttpClientServiceException : Exception
    {
        public HttpClientServiceException(string message) : base(message)
        {

        }
    }
}
