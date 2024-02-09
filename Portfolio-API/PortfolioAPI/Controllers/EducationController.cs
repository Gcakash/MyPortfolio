using MediatR;
using Microsoft.AspNetCore.Mvc;
using Portfolio.API.Common.Models;
using Portfolio.API.MediatR.MediatRService.EducationService;

namespace Portfolio.Api.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]    
    public class EducationController : ControllerBase
    {
       private readonly IMediator _mediator;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mediator"></param>
        public EducationController(IMediator mediator)
        {
            _mediator = mediator;
        }
        /// <summary>
        /// Education index
        /// </summary>
        /// <returns> List of Education </returns>

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var response = await _mediator.Send(new GetEducationsQuery());
            if (response != null)
            {
                return Ok(response);
            }
            return BadRequest();
        }
         /// <summary>
         /// Get the Education 
         /// </summary>
         /// <param name="id"></param>
         /// <returns>Education details</returns>

         [HttpGet("{id}")]
         public async Task<IActionResult> GetByIdAsync(int id)
         {
             var response = await _mediator.Send(new GetEducationByIdQuery(id));
             if(response != null)
             {
                 return Ok(response);
             }
             return BadRequest();

         }
         /// <summary>
         /// Update the Education details
         /// </summary>
         /// <param name="model"></param>
         /// <param name="id"></param>
         /// <returns>Education details</returns>
         [HttpPut("{id}")]
         public async Task<IActionResult> UpdateAsync([FromBody] EducationModel model, int id)
         {
             if (ModelState.IsValid)
             {
                 var response = await _mediator.Send(new UpdateEducationCommand(id, model));
                 if (response != null)
                 {
                     return Ok(response);
                 }
             }
             return BadRequest();
         }
         /// <summary>
         /// Delete the Education
         /// </summary>
         /// <param name="id"></param>
         /// <returns>True or False</returns>

         [HttpDelete("{id}")]
         public async Task<IActionResult> DeleteAsync(int id)
         {
             var response = await _mediator.Send(new DeleteEducationCommand(id));
             if (response != null)
             {
                 return Ok(response);
             }
             return BadRequest();
         }

        /// <summary>
        /// Create Education
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Education details</returns>

         [HttpPost("Admin")]
         public async Task<IActionResult> InsertByAdminAsync(EducationModel model)
         {
             if (ModelState.IsValid)
             {
                 var response = await _mediator.Send(new InsertEducationCommand(model));
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
         public async Task<IActionResult> GetActiveEducationAsync()
        {
            if (ModelState.IsValid)
            {
                 var response = await _mediator.Send(new GetActiveEducationsQuery());
                 return Ok(response);
             }
             return BadRequest();
         }

    }
}
