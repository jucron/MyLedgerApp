using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyLedgerApp.Api.v1.Models;
using MyLedgerApp.Application.Services;
using MyLedgerApp.Application.Validation;
using MyLedgerApp.Application.Validation.User;
using MyLedgerApp.Common.Utils;
using MyLedgerApp.Domain.Entities;
using static MyLedgerApp.Common.Utils.Exceptions;

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
        /// <returns></returns>
        [HttpGet]
        [Route("")]
        public ActionResult<IEnumerable<UserDTO>> GetUsers([FromQuery] UserType type)
        {
            NotNullEnumValidator.Run(type);
            return Ok(_userService.GetUsers(type));
        }

        /// <summary>
        /// Get a single User.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}")]
        public ActionResult<UserDTO> GetUser(Guid id)
        {
            NotEmptyGuidValidator.Run(id);
            return Ok(_userService.GetUserById(id));
        }

        /// <summary>
        /// [OPEN] Add a single User. UserType can be either Employee or Client.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("")]
        [AllowAnonymous]
        public ActionResult<UserDTO> AddUser(UserRequest request)
        {
            AddUserValidator.Run(request);
            return Ok(_userService.AddUser(request));
        }

        /// <summary>
        /// Update a single User.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("{id}")]
        public ActionResult<UserDTO> UpdateUser(Guid id, UserDTO user)
        {
            NotEmptyGuidValidator.Run(id);
            UpdateUserValidator.Run(user);
            return Ok(_userService.UpdateUser(id,user));

        }

        /// <summary>
        /// Delete a single User.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{id}")]
        public ActionResult DeleteUser(Guid id)
        {
            NotEmptyGuidValidator.Run(id);
            _userService.DeleteUser(id);
            return Ok();
        }
    }
}
