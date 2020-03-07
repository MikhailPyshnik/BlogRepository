using System;
using System.Collections.Generic;
using System.Text;

namespace BlogApi.Models.Comment
{
    public class CommentResponce
    {
        public string Text { get; set; } = string.Empty;

        public string UserName { get; set; } = string.Empty;

        public DateTime UpdatedOn { get; set; }
    }
}
