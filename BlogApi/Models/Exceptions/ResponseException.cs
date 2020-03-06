using System;
using System.Collections.Generic;
using System.Text;

namespace Models.Exeptions
{
    public class ResponseException : GeneralException
    {
        public ResponseException(string message, int statusCode) : base(message, statusCode)
        {
        }
    }
}
