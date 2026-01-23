using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyLedgerApp.Api.v1.Models;
using MyLedgerApp.Application.Services;
using MyLedgerApp.Application.Validation;
using MyLedgerApp.Application.Validation.User;
using MyLedgerApp.Domain.Entities.Users;

namespace MyLedgerApp.Api.v1.Controllers
{
    [ApiController]
    [Route("api/v1/users")]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUserService _userService;

        public UserController(ILogger<UserController> logger, IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        /// <summary>
        /// Get all Users.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("")]
        public async Task<ActionResult<IEnumerable<UserDTO>>> GetUsers([FromQuery] UserType type)
        {
            NotNullEnumValidator.Run(type);
            return Ok(await _userService.GetUsers(type));
        }

        /// <summary>
        /// Get a single User.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<UserDTO>> GetUser(Guid id)
        {
            NotEmptyGuidValidator.Run(id);
            return Ok(await _userService.GetUserById(id));
        }

        /// <summary>
        /// [OPEN] Add a single User. UserType can be either Employee or Client.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("")]
        [AllowAnonymous]
        public async Task<ActionResult<UserDTO>> AddUser(UserRequest request)
        {
            AddUserValidator.Run(request);
            return Ok(await _userService.AddUser(request));
        }

        /// <summary>
        /// Update a single User. 
        /// Note that User's Credentials cannot be updated here.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="user"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("{id}")]
        public async Task<ActionResult<UserDTO>> UpdateUser(Guid id, UserDTO user)
        {
            NotEmptyGuidValidator.Run(id);
            UpdateUserValidator.Run(user);
            return Ok(await _userService.UpdateUser(id, user));

        }

        /// <summary>
        /// Delete a single User.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult> DeleteUser(Guid id)
        {
            NotEmptyGuidValidator.Run(id);
            await _userService.DeleteUser(id);
            return Ok();
        }
    }
}
