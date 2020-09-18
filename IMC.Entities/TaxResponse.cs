using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace IMC.Entities
{
    public class TaxResponse<T> where T : class
    {
        public TaxResponse(T data, HttpStatusCode errorCode, string message)
        {
            Data = data;
            ErrorCode = errorCode;
            Message = message;
        }

        public T Data { get; set; }
        public string Message { get; set; }
        public HttpStatusCode ErrorCode { get; set; }
    }
}
