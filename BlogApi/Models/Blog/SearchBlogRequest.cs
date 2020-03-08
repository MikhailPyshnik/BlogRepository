using System.ComponentModel.DataAnnotations;

namespace BlogApi.Models.Blog
{
    public class SearchBlogRequest
    {
        [Required]
        public string SearchString {get; set; }
    }
}
