using MediatR;
using Microsoft.AspNetCore.Mvc;
using Portfolio.API.Common.Models;
using Portfolio.API.MediatR.MediatRService.SocialMediaService;

namespace Portfolio.Api.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]    
    public class SocialMediaController : ControllerBase
    {
       private readonly IMediator _mediator;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mediator"></param>
        public SocialMediaController(IMediator mediator)
        {
            _mediator = mediator;
        }
        /// <summary>
        /// SocialMedia index
        /// </summary>
        /// <returns> List of SocialMedia </returns>

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var response = await _mediator.Send(new GetSocialMediasQuery());
            if (response != null)
            {
                return Ok(response);
            }
            return BadRequest();
        }
         /// <summary>
         /// Get the SocialMedia 
         /// </summary>
         /// <param name="id"></param>
         /// <returns>SocialMedia details</returns>

         [HttpGet("{id}")]
         public async Task<IActionResult> GetByIdAsync(int id)
         {
             var response = await _mediator.Send(new GetSocialMediaByIdQuery(id));
             if(response != null)
             {
                 return Ok(response);
             }
             return BadRequest();

         }
         /// <summary>
         /// Update the SocialMedia details
         /// </summary>
         /// <param name="model"></param>
         /// <param name="id"></param>
         /// <returns>SocialMedia details</returns>
         [HttpPut("{id}")]
         public async Task<IActionResult> UpdateAsync([FromBody] SocialMediaModel model, int id)
         {
             if (ModelState.IsValid)
             {
                 var response = await _mediator.Send(new UpdateSocialMediaCommand(id, model));
                 if (response != null)
                 {
                     return Ok(response);
                 }
             }
             return BadRequest();
         }
         /// <summary>
         /// Delete the SocialMedia
         /// </summary>
         /// <param name="id"></param>
         /// <returns>True or False</returns>

         [HttpDelete("{id}")]
         public async Task<IActionResult> DeleteAsync(int id)
         {
             var response = await _mediator.Send(new DeleteSocialMediaCommand(id));
             if (response != null)
             {
                 return Ok(response);
             }
             return BadRequest();
         }

        /// <summary>
        /// Create SocialMedia
        /// </summary>
        /// <param name="model"></param>
        /// <returns>SocialMedia details</returns>

         [HttpPost("Admin")]
         public async Task<IActionResult> InsertByAdminAsync(SocialMediaModel model)
         {
             if (ModelState.IsValid)
             {
                 var response = await _mediator.Send(new InsertSocialMediaCommand(model));
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
         public async Task<IActionResult> GetActiveSocialMediaAsync()
        {
            if (ModelState.IsValid)
            {
                 var response = await _mediator.Send(new GetActiveSocialMediasQuery());
                 return Ok(response);
             }
             return BadRequest();
         }

    }
}
