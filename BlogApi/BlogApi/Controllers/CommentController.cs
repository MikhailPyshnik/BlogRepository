using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using BlogApi.Models.Comment;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.Comment;
using Services.CommentService;

namespace BlogApi.Controllers
{
    [Authorize]
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService _commentService;
        private readonly IMapper _mapper;

        public CommentController(ICommentService commentService, IMapper mapper)
        {
            _commentService = commentService;
            _mapper = mapper;
        }

        /// <summary>
        /// Gets all comments by blog.
        /// </summary>
        /// <param name="blogId">BlogId.</param>
        /// <returns>All available comments.</returns>
        /// <response code="200">Return comment.</response>
        /// <response code="404">Not Found.</response>
        /// <response code="500">Server error.</response>
        [AllowAnonymous]
        [HttpGet("{blogId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<CommentModel>>> GetComments(string blogId)
        {
            await _commentService.GetCurrentBlogAsync(blogId);
            var comments = await _commentService.GetCommentsAsync();
            return Ok(comments);
        }

        /// <summary>
        /// Get comment by blog.
        /// </summary>
        /// <param name="blogId">BlogId.</param>
        /// <param name="commnetId">CommnetId.</param>
        /// <returns>All available comments.</returns>
        /// <response code="200">Return comment.</response>
        /// <response code="404">Not Found.</response>
        /// <response code="500">Server error.</response>
        [AllowAnonymous]
        [HttpGet("{blogId}/{commnetId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<CommentModel>> GetComment(string blogId, string commnetId)
        {

            await _commentService.GetCurrentBlogAsync(blogId);
            var comment = await _commentService.GetCommentAsync(commnetId);
            return Ok(comment);
        }

        /// <summary>
        /// Adds a new comment.
        /// </summary>
        /// <param name="blogId"> Blog id.</param>
        /// <param name="commentRequest"> Add new comment.</param>
        /// <returns>A <see cref="CreatedAtActionResult"/>.</returns>
        /// <response code="201">Returns a new comment.</response>
        /// <response code="400">Not valid.</response>
        /// <response code="401">User is unauthorized.</response>
        /// <response code="500">Server error.</response>
        [HttpPost("{blogId}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<CommentModel>> AddComment(string blogId, [FromBody] CreateComment commentRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _commentService.GetCurrentBlogAsync(blogId);

            var commentMapper = _mapper.Map<CreateComment, UpdateCommentRequest>(commentRequest);

            commentMapper.UserName = HttpContext.User.Identity.Name;

            var comment = await _commentService.CreateCommentAsync(commentMapper);

            var uri = $"/comments/{comment.CreatedOn.ToString("MM/dd/yyyy/HH:mm:ss.fff")}";
            return Created(uri, comment);
        }

        /// <summary>
        /// Updates comment by id.
        /// </summary>
        /// <param name="blogId"> blogId.</param>
        /// <param name=" commentId"> commentId.</param>
        /// <param name="commentRequest"> Add update comment.</param>
        /// <returns>A <see cref="Task{ActionResult}"/>.</returns>
        /// <response code="204">NoContent.</response>
        /// <response code="400">Not valid.</response>
        /// <response code="401">User is unauthorized.</response>
        /// <response code="404">Not Found.</response>
        /// <response code="500">Server error.</response>
        [HttpPut("{blogId}/{commnetId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> UpdateComment(string blogId, string commentId, [FromBody] UpdateCommentRequest commentRequest)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _commentService.GetCurrentBlogAsync(blogId);

            var comment = await _commentService.UpdateCommentAsync(commentId, commentRequest);

            //var uri = $"/comments/{comment.CreatedOn.ToString("MM/dd/yyyy/HH:mm:ss.fff")}";
            //return Created(uri, comment);
            return NoContent();
        }

        /// <summary>
        /// Delete commnet in blog by commentId.
        /// </summary>
        /// <param name="blogId"> blogId.</param>
        /// <param name="commentId"> commentId.</param>
        /// <returns>A <see cref="Task{ActionResult}"/>.</returns>
        /// <response code="200">Return text.</response>
        /// <response code="401">User is unauthorized.</response>
        /// <response code="404">Not Found.</response>
        /// <response code="500">Server error.</response>
        [HttpDelete("{blogId}/{commnetId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> DeleteComment(string blogId, string commentId)
        {
            await _commentService.GetCurrentBlogAsync(blogId);
            await _commentService.DeleteCommentAsync(commentId);

            return Ok("Commnet was deleted."); ;
        }
    }
}