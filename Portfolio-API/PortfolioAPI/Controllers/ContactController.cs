using MediatR;
using Microsoft.AspNetCore.Mvc;
using Portfolio.API.Common.Models;
using Portfolio.API.MediatR.MediatRService.ContactService;

namespace Portfolio.Api.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]    
    public class ContactController : ControllerBase
    {
       private readonly IMediator _mediator;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mediator"></param>
        public ContactController(IMediator mediator)
        {
            _mediator = mediator;
        }
        /// <summary>
        /// Contact index
        /// </summary>
        /// <returns> List of Contact </returns>

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var response = await _mediator.Send(new GetContactsQuery());
            if (response != null)
            {
                return Ok(response);
            }
            return BadRequest();
        }
         /// <summary>
         /// Get the Contact 
         /// </summary>
         /// <param name="id"></param>
         /// <returns>Contact details</returns>

         [HttpGet("{id}")]
         public async Task<IActionResult> GetByIdAsync(int id)
         {
             var response = await _mediator.Send(new GetContactByIdQuery(id));
             if(response != null)
             {
                 return Ok(response);
             }
             return BadRequest();

         }
         /// <summary>
         /// Update the Contact details
         /// </summary>
         /// <param name="model"></param>
         /// <param name="id"></param>
         /// <returns>Contact details</returns>
         [HttpPut("{id}")]
         public async Task<IActionResult> UpdateAsync([FromBody] ContactModel model, int id)
         {
             if (ModelState.IsValid)
             {
                 var response = await _mediator.Send(new UpdateContactCommand(id, model));
                 if (response != null)
                 {
                     return Ok(response);
                 }
             }
             return BadRequest();
         }
         /// <summary>
         /// Delete the Contact
         /// </summary>
         /// <param name="id"></param>
         /// <returns>True or False</returns>

         [HttpDelete("{id}")]
         public async Task<IActionResult> DeleteAsync(int id)
         {
             var response = await _mediator.Send(new DeleteContactCommand(id));
             if (response != null)
             {
                 return Ok(response);
             }
             return BadRequest();
         }

        /// <summary>
        /// Create Contact
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Contact details</returns>

         [HttpPost("Admin")]
         public async Task<IActionResult> InsertByAdminAsync(ContactModel model)
         {
             if (ModelState.IsValid)
             {
                 var response = await _mediator.Send(new InsertContactByAdminCommand(model));
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
         public async Task<IActionResult> GetActiveContactAsync()
         {
             if (ModelState.IsValid)
             {
                 var response = await _mediator.Send(new GetActiveContactQuery());
                 return Ok(response);
             }
             return BadRequest();
         }

    }
}
