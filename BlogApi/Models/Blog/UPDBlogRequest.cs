using System.Collections.Generic;

namespace Models.Blog
{
    public class UPDBlogRequest
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public string UserName { get; set; }
        public int UserId { get; set; }
        public IEnumerable<Models.Comment.Comment> Commets { get; set; } = new IEnumerable<Models.Comment.Comment>();
    }
}
