using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyLedgerApp.Api.v1.Models;
using MyLedgerApp.Application.Services;
using MyLedgerApp.Application.Validation;
using MyLedgerApp.Application.Validation.User;

namespace MyLedgerApp.Api.v1.Controllers
{
    [ApiController]
    [Route("api/v1/employees")]
    [Authorize]
    public class EmployeeController : ControllerBase
    {
        private readonly IUserService _userService;

        public EmployeeController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Get all Employees.
        /// </summary>
        /// <returns>List of Employees DTOs</returns>
        [HttpGet]
        [Route("")]
        public async Task<ActionResult<IEnumerable<EmployeeDTO>>> GetEmployees()
        {
            return Ok(await _userService.GetEmployees());
        }

        /// <summary>
        /// Get a single Employee.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Employee DTO</returns>
        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<EmployeeDTO>> GetEmployee(Guid id)
        {
            GuidValidator.Run(id);
            return Ok(await _userService.GetEmployee(id));
        }

        /// <summary>
        /// [OPEN] Add a single Employee.
        /// </summary>
        /// <param name="request"></param>
        /// <returns>Employee DTO</returns>
        [HttpPost]
        [Route("")]
        [AllowAnonymous]
        public async Task<ActionResult<EmployeeDTO>> AddEmployee(AddEmployeeRequest request)
        {
            AddEmployeeValidator.Run(request);
            var user = await _userService.AddEmployee(request);
            return CreatedAtAction(nameof(GetEmployee), new {id = user.Id }, user);
        }

        /// <summary>
        /// Update a single Employee. 
        /// Note that User's Credentials cannot be updated here.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updateReq"></param>
        /// <returns>Employee DTO</returns>
        [HttpPut]
        [Route("{id}")]
        public async Task<ActionResult<EmployeeDTO>> UpdateEmployee(Guid id, UpdateEmployeeRequest updateReq)
        {
            GuidValidator.Run(id);
            UpdateEmployeeValidator.Run(updateReq);
            return Ok(await _userService.UpdateEmployee(id, updateReq));
        }

        /// <summary>
        /// Delete a single Employee.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Nothing</returns>
        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult> DeleteEmployee(Guid id)
        {
            GuidValidator.Run(id);
            await _userService.DeleteEmployee(id);
            return NoContent();
        }
    }
}
