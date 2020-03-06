using System;

namespace Models.Exeptions
{
    public class GeneralException : Exception
    {
        public string Message { get; }
        public int StatusCode { get; }

        public GeneralException(string message, int statusCode)
        {
            Message = message;
            StatusCode = statusCode;
        }
    }
}
