using MediatR;
using Microsoft.AspNetCore.Mvc;
using Portfolio.API.Common.Models;
using Portfolio.API.MediatR.MediatRService.BlogCommentService;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Portfolio.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogCommentController : ControllerBase
    {
        private readonly IMediator _mediator;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mediator"></param>
        public BlogCommentController(IMediator mediator)
        {
            _mediator = mediator;
        }
        /// <summary>
        /// BlogComment index
        /// </summary>
        /// <returns> List of BlogComment </returns>

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var response = await _mediator.Send(new GetBlogCommentQuery());
            if (response != null)
            {
                return Ok(response);
            }
            return BadRequest();
        }
        /// <summary>
        /// Get the BlogComment 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>BlogComment details</returns>

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var response = await _mediator.Send(new GetBlogCommentByIdQuery(id));
            if (response != null)
            {
                return Ok(response);
            }
            return BadRequest();

        }
        /// <summary>
        /// Update the BlogComment details
        /// </summary>
        /// <param name="model"></param>
        /// <param name="id"></param>
        /// <returns>BlogComment details</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync([FromBody] BlogCommentModel model, int id)
        {
            if (ModelState.IsValid)
            {
                var response = await _mediator.Send(new UpdateBlogCommentCommand(id, model));
                if (response != null)
                {
                    return Ok(response);
                }
            }
            return BadRequest();
        }
        /// <summary>
        /// Delete the BlogComment
        /// </summary>
        /// <param name="id"></param>
        /// <returns>True or False</returns>

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var response = await _mediator.Send(new DeleteBlogCommentCommand(id));
            if (response != null)
            {
                return Ok(response);
            }
            return BadRequest();
        }

        /// <summary>
        /// Create BlogComment
        /// </summary>
        /// <param name="model"></param>
        /// <returns>BlogComment details</returns>

        [HttpPost("insert")]
        public async Task<IActionResult> InsertBlogCommentAsync(BlogCommentModel model)
        {
            if (ModelState.IsValid)
            {
                var response = await _mediator.Send(new InsertBlogCommentCommand(model));
                if (response != null)
                {
                    return Ok(response);
                }
            }
            return BadRequest();
        }

    }
}

