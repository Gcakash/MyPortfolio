using MediatR;
using Microsoft.AspNetCore.Mvc;
using Portfolio.API.Common.Models;
using Portfolio.API.MediatR.MediatRService.WorkExperienceService;

namespace Portfolio.Api.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]    
    public class WorkExperienceController : ControllerBase
    {
       private readonly IMediator _mediator;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mediator"></param>
        public WorkExperienceController(IMediator mediator)
        {
            _mediator = mediator;
        }
        /// <summary>
        /// WorkExperience index
        /// </summary>
        /// <returns> List of WorkExperience </returns>

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var response = await _mediator.Send(new GetWorkExperiencesQuery());
            if (response != null)
            {
                return Ok(response);
            }
            return BadRequest();
        }
         /// <summary>
         /// Get the WorkExperience 
         /// </summary>
         /// <param name="id"></param>
         /// <returns>WorkExperience details</returns>

         [HttpGet("{id}")]
         public async Task<IActionResult> GetByIdAsync(int id)
         {
             var response = await _mediator.Send(new GetWorkExperienceByIdQuery(id));
             if(response != null)
             {
                 return Ok(response);
             }
             return BadRequest();

         }
         /// <summary>
         /// Update the WorkExperience details
         /// </summary>
         /// <param name="model"></param>
         /// <param name="id"></param>
         /// <returns>WorkExperience details</returns>
         [HttpPut("{id}")]
         public async Task<IActionResult> UpdateAsync([FromBody] WorkExperienceModel model, int id)
         {
             if (ModelState.IsValid)
             {
                 var response = await _mediator.Send(new UpdateWorkExperienceCommand(id, model));
                 if (response != null)
                 {
                     return Ok(response);
                 }
             }
             return BadRequest();
         }
         /// <summary>
         /// Delete the WorkExperience
         /// </summary>
         /// <param name="id"></param>
         /// <returns>True or False</returns>

         [HttpDelete("{id}")]
         public async Task<IActionResult> DeleteAsync(int id)
         {
             var response = await _mediator.Send(new DeleteWorkExperienceCommand(id));
             if (response != null)
             {
                 return Ok(response);
             }
             return BadRequest();
         }

        /// <summary>
        /// Create WorkExperience
        /// </summary>
        /// <param name="model"></param>
        /// <returns>WorkExperience details</returns>

         [HttpPost("Admin")]
         public async Task<IActionResult> InsertByAdminAsync(WorkExperienceModel model)
         {
             if (ModelState.IsValid)
             {
                 var response = await _mediator.Send(new InsertWorkExperienceCommand(model));
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
         public async Task<IActionResult> GetActiveWorkExperienceAsync()
        {
            if (ModelState.IsValid)
            {
                 var response = await _mediator.Send(new GetActiveWorkExperiencesQuery());
                 return Ok(response);
             }
             return BadRequest();
         }

    }
}
