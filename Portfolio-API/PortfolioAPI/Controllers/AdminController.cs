using MediatR;
using Microsoft.AspNetCore.Mvc;
using Portfolio.API.Common.Models;
using Portfolio.API.MediatR.MediatRService.AdminService;

namespace Portfolio.Api.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class AdminController : ControllerBase
    {
        private readonly IMediator _mediator;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mediator"></param>
        public AdminController(IMediator mediator)
        {
            _mediator = mediator;
        }
        /// <summary>
        /// Admin index
        /// </summary>
        /// <returns> List of Admin </returns>

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var response = await _mediator.Send(new GetAdminQuery());
            if (response != null)
            {
                return Ok(response);
            }
            return BadRequest();
        }
        /// <summary>
        /// Get the Admin 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Admin details</returns>

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var response = await _mediator.Send(new GetAdminByIdQuery(id));
            if (response != null)
            {
                return Ok(response);
            }
            return BadRequest();

        }
        /// <summary>
        /// Update the Admin details
        /// </summary>
        /// <param name="model"></param>
        /// <param name="id"></param>
        /// <returns>Admin details</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync([FromBody] AdminModel model, int id)
        {
            if (ModelState.IsValid)
            {
                var response = await _mediator.Send(new UpdateAdminCommand(id, model));
                if (response != null)
                {
                    return Ok(response);
                }
            }
            return BadRequest();
        }
        /// <summary>
        /// Delete the Admin
        /// </summary>
        /// <param name="id"></param>
        /// <returns>True or False</returns>

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var response = await _mediator.Send(new DeleteAdminCommand(id));
            if (response != null)
            {
                return Ok(response);
            }
            return BadRequest();
        }

        /// <summary>
        /// Create Admin
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Admin details</returns>

        [HttpPost("Admin")]
        public async Task<IActionResult> InsertByAdminAsync(AdminModel model)
        {
            if (ModelState.IsValid)
            {
                var response = await _mediator.Send(new InsertAdminCommand(model));
                if (response != null)
                {
                    return Ok(response);
                }
            }
            return BadRequest();
        }

    }
}
