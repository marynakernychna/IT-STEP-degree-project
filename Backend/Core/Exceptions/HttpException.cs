﻿using System;
using System.Net;

namespace Core.Exceptions
{
    [Serializable]
    public class HttpException : ApplicationException
    {
        public HttpStatusCode StatusCode { get; set; }

        public HttpException() { }
        public HttpException(string message, HttpStatusCode statusCode) : base(message)
        {
            this.StatusCode = statusCode;
        }
        public HttpException(string message, Exception inner) : base(message, inner) { }
        protected HttpException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
