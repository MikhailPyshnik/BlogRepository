using System;
using System.Collections.Generic;
using System.Text;

namespace Models.Exeptions
{
    public class RequestException : GeneralException
    {
        public RequestException(string message) : base(message, 400)
        {
        }
    }
}
