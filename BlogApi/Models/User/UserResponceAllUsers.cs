using System;
using System.Collections.Generic;
using System.Text;

namespace BlogApi.Models.User
{
    public class UserResponceAllUsers
    {
        public string Email { get; set; }
        public string UserName { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
