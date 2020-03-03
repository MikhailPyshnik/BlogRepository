using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Blog;
using Services.BlogService;

namespace BlogApi.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        private readonly IBlogService _blockService;
        public BlogController(IBlogService blockService)
        {
            _blockService = blockService;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Blog>>> GetArticles()
        {
            var articles = await _blockService.GetBlogsAsync();
            return Ok(articles);
        }

        [AllowAnonymous]
        [HttpGet("{blogId}")]
        public async Task<ActionResult<Blog>> GetBlock(string blogId)
        {
            var article = await _blockService.GetBlogAsync(blogId);

            return Ok(article);
        }

        [HttpPost]
        public async Task<ActionResult<Blog>> AddBlock([FromBody] UPDBlogRequest blockRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

           // blockRequest.UserName = HttpContext.User.Identity.Name;
           // blockRequest.UserName = User.Identity.Name;

            var block = await _blockService.CreateBlogAsync(blockRequest);

            var uri = $"/blocks/{block.Id}";
            return Created(uri, block);
        }

        [HttpPut("{blogId}")]
        public async Task<ActionResult> UpdateBlock(string blogId, [FromBody] UPDBlogRequest blockRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _blockService.UpdateBlogAsync(blogId, blockRequest);

            return NoContent();
        }

        [HttpDelete("{blogid}")]
        public async Task<ActionResult> DeleteBlock(string blogid)
        {
            await _blockService.DeleteBlogAsync(blogid);

            return Ok();
        }
    }
}