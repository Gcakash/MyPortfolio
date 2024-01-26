using MediatR;
using Microsoft.AspNetCore.Mvc;
using Portfolio.API.Common.Models;
using Portfolio.API.MediatR.MediatRService.UserService;

namespace Portfolio.Api.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]    
    public class UserController : ControllerBase
    {
       private readonly IMediator _mediator;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mediator"></param>
        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }
        /// <summary>
        /// User index
        /// </summary>
        /// <returns> List of User </returns>

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var response = await _mediator.Send(new GetUsersQuery());
            if (response != null)
            {
                return Ok(response);
            }
            return BadRequest();
        }
         /// <summary>
         /// Get the user 
         /// </summary>
         /// <param name="id"></param>
         /// <returns>User details</returns>

         [HttpGet("{id}")]
         public async Task<IActionResult> GetByIdAsync(int id)
         {
             var response = await _mediator.Send(new GetUserByIdQuery(id));
             if(response != null)
             {
                 return Ok(response);
             }
             return BadRequest();

         }
         /// <summary>
         /// Update the User details
         /// </summary>
         /// <param name="model"></param>
         /// <param name="id"></param>
         /// <returns>User details</returns>
         [HttpPut("{id}")]
         public async Task<IActionResult> UpdateAsync([FromBody] UserInfoModel model, int id)
         {
             if (ModelState.IsValid)
             {
                 var response = await _mediator.Send(new UpdateUserCommand(id, model));
                 if (response != null)
                 {
                     return Ok(response);
                 }
             }
             return BadRequest();
         }
         /// <summary>
         /// Delete the User
         /// </summary>
         /// <param name="id"></param>
         /// <returns>True or False</returns>

         [HttpDelete("{id}")]
         public async Task<IActionResult> DeleteAsync(int id)
         {
             var response = await _mediator.Send(new DeleteUserCommand(id));
             if (response != null)
             {
                 return Ok(response);
             }
             return BadRequest();
         }

        /// <summary>
        /// Create User
        /// </summary>
        /// <param name="model"></param>
        /// <returns>User details</returns>

         [HttpPost("Admin")]
         public async Task<IActionResult> InsertByAdminAsync(UserInfoModel model)
         {
             if (ModelState.IsValid)
             {
                 var response = await _mediator.Send(new InsertUserByAdminCommand(model));
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
        
         [HttpPost]
         [Route("Filter")]
         public async Task<IActionResult> GetActiveUserAsync()
         {
             if (ModelState.IsValid)
             {
                 var response = await _mediator.Send(new GetActiveUserQuery());
                 return Ok(response);
             }
             return BadRequest();
         }

    }
}
