using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Comment;
using Services.CommentService;

namespace BlogApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService _commentService;

        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        [AllowAnonymous]
        [HttpGet("{blogId}")]
        public async Task<ActionResult<IEnumerable<Comment>>> GetComments(string blogId)
        {
            await _commentService.GetCurrentBlogAsync(blogId);
            var comments = await _commentService.GetCommentsAsync();
            return Ok(comments);
        }

        [AllowAnonymous]
        [HttpGet("{blogId}/{commnetId}")]
        public async Task<ActionResult<Comment>> GetComment(string blogId, string commnetId)
        {

            await _commentService.GetCurrentBlogAsync(blogId);
            var comment = await _commentService.GetCommentAsync(commnetId);
            return Ok(comment);
        }

        [HttpPost("{blogId}")]
        public async Task<ActionResult<Comment>> AddComment(string blogId, [FromBody] UPDCommentRequest commentRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _commentService.GetCurrentBlogAsync(blogId);


            commentRequest.UserName = HttpContext.User.Identity.Name;

            var comment = await _commentService.CreateCommentAsync(commentRequest);

            var uri = $"/comments/{comment.CreatedOn.ToString("MM/dd/yyyy/HH:mm:ss.fff")}";
            return Created(uri, comment);
        }

        [HttpPut("{blogId}/{commnetId}")]
        public async Task<ActionResult> UpdateComment(string blogId,string commentId, [FromBody] UPDCommentRequest commentRequest)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _commentService.GetCurrentBlogAsync(blogId);

            var comment = await _commentService.UpdateCommentAsync(commentId,commentRequest);

            var uri = $"/comments/{comment.CreatedOn.ToString("MM/dd/yyyy/HH:mm:ss.fff")}";
            return Created(uri, comment);
        }

        [HttpDelete("{blogId}/{commnetId}")]
        public async Task<ActionResult> DeleteComment(string blogId, string commentId)
        {
            await _commentService.GetCurrentBlogAsync(blogId);
            await _commentService.DeleteCommentAsync(commentId);

            return Ok();
        }
    }
}