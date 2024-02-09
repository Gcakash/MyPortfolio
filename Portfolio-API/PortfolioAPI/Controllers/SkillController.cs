using MediatR;
using Microsoft.AspNetCore.Mvc;
using Portfolio.API.Common.Models;
using Portfolio.API.MediatR.MediatRService.SkillService;

namespace Portfolio.Api.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]    
    public class SkillController : ControllerBase
    {
       private readonly IMediator _mediator;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mediator"></param>
        public SkillController(IMediator mediator)
        {
            _mediator = mediator;
        }
        /// <summary>
        /// Skill index
        /// </summary>
        /// <returns> List of Skill </returns>

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var response = await _mediator.Send(new GetSkillsQuery());
            if (response != null)
            {
                return Ok(response);
            }
            return BadRequest();
        }
         /// <summary>
         /// Get the Skill 
         /// </summary>
         /// <param name="id"></param>
         /// <returns>Skill details</returns>

         [HttpGet("{id}")]
         public async Task<IActionResult> GetByIdAsync(int id)
         {
             var response = await _mediator.Send(new GetSkillByIdQuery(id));
             if(response != null)
             {
                 return Ok(response);
             }
             return BadRequest();

         }
         /// <summary>
         /// Update the Skill details
         /// </summary>
         /// <param name="model"></param>
         /// <param name="id"></param>
         /// <returns>Skill details</returns>
         [HttpPut("{id}")]
         public async Task<IActionResult> UpdateAsync([FromBody] SkillModel model, int id)
         {
             if (ModelState.IsValid)
             {
                 var response = await _mediator.Send(new UpdateSkillCommand(id, model));
                 if (response != null)
                 {
                     return Ok(response);
                 }
             }
             return BadRequest();
         }
         /// <summary>
         /// Delete the Skill
         /// </summary>
         /// <param name="id"></param>
         /// <returns>True or False</returns>

         [HttpDelete("{id}")]
         public async Task<IActionResult> DeleteAsync(int id)
         {
             var response = await _mediator.Send(new DeleteSkillCommand(id));
             if (response != null)
             {
                 return Ok(response);
             }
             return BadRequest();
         }

        /// <summary>
        /// Create Skill
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Skill details</returns>

         [HttpPost("Admin")]
         public async Task<IActionResult> InsertByAdminAsync(SkillModel model)
         {
             if (ModelState.IsValid)
             {
                 var response = await _mediator.Send(new InsertSkillCommand(model));
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
         public async Task<IActionResult> GetActiveSkillAsync()
        {
            if (ModelState.IsValid)
            {
                 var response = await _mediator.Send(new GetActiveSkillsQuery());
                 return Ok(response);
             }
             return BadRequest();
         }

    }
}
