using System;
using System.Net;

namespace Core.Exceptions
{
    [Serializable]
    public class HttpException : ApplicationException
    {
        public HttpStatusCode HttpStatusCode { get; set; }

        public HttpException() { }

        public HttpException(
            string message,
            HttpStatusCode statusCode) : base(message)
        {
            HttpStatusCode = statusCode;
        }

        public HttpException(
            string message,
            Exception exception) : base(message, exception) { }

        protected HttpException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
