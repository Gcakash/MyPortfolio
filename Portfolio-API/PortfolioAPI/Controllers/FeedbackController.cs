using MediatR;
using Microsoft.AspNetCore.Mvc;
using Portfolio.API.Common.Models;
using Portfolio.API.MediatR.MediatRService.FeedbackService;

namespace Portfolio.Api.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]    
    public class FeedbackController : ControllerBase
    {
       private readonly IMediator _mediator;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mediator"></param>
        public FeedbackController(IMediator mediator)
        {
            _mediator = mediator;
        }
        /// <summary>
        /// Feedback index
        /// </summary>
        /// <returns> List of Feedback </returns>

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var response = await _mediator.Send(new GetFeedbacksQuery());
            if (response != null)
            {
                return Ok(response);
            }
            return BadRequest();
        }
         /// <summary>
         /// Get the Feedback 
         /// </summary>
         /// <param name="id"></param>
         /// <returns>Feedback details</returns>

         [HttpGet("{id}")]
         public async Task<IActionResult> GetByIdAsync(int id)
         {
             var response = await _mediator.Send(new GetFeedbackByIdQuery(id));
             if(response != null)
             {
                 return Ok(response);
             }
             return BadRequest();

         }
         /// <summary>
         /// Update the Feedback details
         /// </summary>
         /// <param name="model"></param>
         /// <param name="id"></param>
         /// <returns>Feedback details</returns>
         [HttpPut("{id}")]
         public async Task<IActionResult> UpdateAsync([FromBody] FeedbackModel model, int id)
         {
             if (ModelState.IsValid)
             {
                 var response = await _mediator.Send(new UpdateFeedbackCommand(id, model));
                 if (response != null)
                 {
                     return Ok(response);
                 }
             }
             return BadRequest();
         }
         /// <summary>
         /// Delete the Feedback
         /// </summary>
         /// <param name="id"></param>
         /// <returns>True or False</returns>

         [HttpDelete("{id}")]
         public async Task<IActionResult> DeleteAsync(int id)
         {
             var response = await _mediator.Send(new DeleteFeedbackCommand(id));
             if (response != null)
             {
                 return Ok(response);
             }
             return BadRequest();
         }

        /// <summary>
        /// Create Feedback
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Feedback details</returns>

         [HttpPost]
         public async Task<IActionResult> InsertAsync(FeedbackModel model)
         {
             if (ModelState.IsValid)
             {
                 var response = await _mediator.Send(new InsertFeedbackCommand(model));
                 if (response != null)
                 {
                     return Ok(response);
                 }
             }
             return BadRequest();
         }

    }
}
