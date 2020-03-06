using System;
using System.Collections.Generic;
using System.Text;

namespace Models.Exeptions
{
    public class NotFoundException : GeneralException
    {
        public NotFoundException(string message) : base(message, 500)
        {

        }
    }
}
