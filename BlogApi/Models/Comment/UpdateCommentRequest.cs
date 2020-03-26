namespace Models.Comment
{
    public class UpdateCommentRequest
    {
        public string Text { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
    }
}
