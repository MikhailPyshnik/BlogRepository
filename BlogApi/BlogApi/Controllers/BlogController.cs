using System.Collections.Generic;
using System.Threading.Tasks;
using BlogApi.Models.Blog;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.Blog;
using Services.BlogService;

namespace BlogApi.Controllers
{
    [Authorize]
    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        private readonly IBlogService _blockService;
        public BlogController(IBlogService blockService)
        {
            _blockService = blockService;
        }

        /// <summary>
        /// Gets all blogs.
        /// </summary>
        /// <returns>All available blogs.</returns>
        /// <response code="200">Returns all blogs.</response>
        /// <response code="500">Server error.</response>
        [AllowAnonymous]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<Blog>>> GetAllBlogs()
        {
            var blogs = await _blockService.GetBlogsAsync();
            return Ok(blogs);
        }

        /// <summary>
        /// Gets blogs by search string.
        /// </summary>
        /// <param name="searchSrting"> earch string.</param>
        /// <returns>All available blogs.</returns>
        /// <response code="200">Return blog.</response>
        /// <response code="404">Not Found.</response>
        /// <response code="500">Server error.</response>
        [AllowAnonymous]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<Blog>>> GetSearchBlog([FromBody] SearchBlogRequest searchSrting)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var blogs = await _blockService.SearchByPartialTitleOccurrenceUserNameOrCategory(searchSrting);
            return Ok(blogs);
        }

        /// <summary>
        /// Gets blog by blogId.
        /// </summary>
        /// <param name="blogId"> blogId.</param>
        /// <returns>Return blog by id.</returns>
        /// <response code="200">Return blog.</response>
        /// <response code="404">Not Found.</response>
        /// <response code="500">Server error.</response>
        [AllowAnonymous]
        [HttpGet("{blogId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Blog>> GetBlock(string blogId)
        {
            var blog = await _blockService.GetBlogAsync(blogId);

            return Ok(blog);
        }


        /// <summary>
        /// Adds new blog.
        /// </summary>
        /// <param name="blockRequest"> Add new blog.</param>
        /// <returns>A <see cref="CreatedAtActionResult"/>.</returns>
        /// <response code="201">Returns a new blog.</response>
        /// <response code="400">Not valid.</response>
        /// <response code="401">User is unauthorized.</response>
        /// <response code="500">Server error.</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Blog>> AddBlock([FromBody] CreateBlogRequest blockRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            blockRequest.UserName = HttpContext.User.Identity.Name;

            var block = await _blockService.CreateBlogAsync(blockRequest);

            var uri = $"/blogs/{block.Id}";
            return Created(uri, block);
        }

        /// <summary>
        /// Updates blog by id.
        /// </summary>
        /// <param name="blogId"> blogId.</param>
        /// <param name="blockRequest"> Add update blog.</param>
        /// <returns>A <see cref="Task{ActionResult}"/>.</returns>
        /// <response code="204">NoContent.</response>
        /// <response code="400">Not valid.</response>
        /// <response code="401">User is unauthorized.</response>
        /// <response code="404">Not Found.</response>
        /// <response code="500">Server error.</response>
        [HttpPut]
        [Route("{blogId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> UpdateBlock(string blogId, [FromBody] UpdateBlogRequest blockRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _blockService.UpdateBlogAsync(blogId, blockRequest);

            return NoContent();
        }

        /// <summary>
        /// Delete blog by blogId.
        /// </summary>
        /// <param name="blogid"> blogId.</param>
        /// <returns>A <see cref="Task{ActionResult}"/>.</returns>
        /// <response code="200">Return text.</response>
        /// <response code="401">User is unauthorized.</response>
        /// <response code="404">Not Found.</response>
        /// <response code="500">Server error.</response>
        [HttpDelete]
        [Route("{blogid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> DeleteBlock(string blogid)
        {
            await _blockService.DeleteBlogAsync(blogid);

            return Ok("Blog was deleted.");
        }
    }
}