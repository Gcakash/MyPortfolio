using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Portfolio.API.Common.Models;
using Portfolio.API.MediatR.MediatRService.BlogPostService;
using Portfolio.API.MediatR.MediatRService.BlogService;
using Portfolio.API.MediatR.MediatRService.EducationService;

namespace Portfolio.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        private readonly IMediator _mediator;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mediator"></param>
        public BlogController(IMediator mediator)
        {
            _mediator = mediator;
        }
        /// <summary>
        /// Blog index
        /// </summary>
        /// <returns> List of Blog </returns>

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var response = await _mediator.Send(new GetBlogQuery());
            if (response != null)
            {
                return Ok(response);
            }
            return BadRequest();
        }
        /// <summary>
        /// Get the Blog 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Blog details</returns>

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var response = await _mediator.Send(new GetBlogByIdQuery(id));
            if (response != null)
            {
                return Ok(response);
            }
            return BadRequest();

        }
        /// <summary>
        /// Update the Blog details
        /// </summary>
        /// <param name="model"></param>
        /// <param name="id"></param>
        /// <returns>Blog details</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync([FromBody] BlogPostModel model, int id)
        {
            if (ModelState.IsValid)
            {
                var response = await _mediator.Send(new UpdateBlogCommand(id, model));
                if (response != null)
                {
                    return Ok(response);
                }
            }
            return BadRequest();
        }
        /// <summary>
        /// Delete the Blog
        /// </summary>
        /// <param name="id"></param>
        /// <returns>True or False</returns>

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var response = await _mediator.Send(new DeleteBlogCommand(id));
            if (response != null)
            {
                return Ok(response);
            }
            return BadRequest();
        }

        /// <summary>
        /// Create Blog
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Blog details</returns>

        [HttpPost("insert")]
        public async Task<IActionResult> InsertBlogAsync(BlogPostModel model)
        {
            if (ModelState.IsValid)
            {
                var response = await _mediator.Send(new InsertBlogCommand(model));
                if (response != null)
                {
                    return Ok(response);
                }
            }
            return BadRequest();
        }



        /// <summary>
        ///
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>

        [HttpGet]
        [Route("Filter")]
        public async Task<IActionResult> GetActiveBlogAsync()
        {
            if (ModelState.IsValid)
            {
                var response = await _mediator.Send(new GetActiveBlogPostsQuery());
                return Ok(response);
            }
            return BadRequest();
        }
    }

}
