using Microsoft.AspNetCore.Mvc;
using MyLedgerApp.Api.v1.Models;
using MyLedgerApp.Domain;
using MyLedgerApp.Services;
using MyLedgerApp.Utils;
using static MyLedgerApp.Utils.Exceptions;

namespace MyLedgerApp.Controllers
{
    /*
     * endpoints:
     * - GET api/v1/users
     * - GET api/v1/users/{id}
     * - POST api/v1/users
     * - PUT api/v1/users/{id}
     * - DELETE api/v1/users/{id}
     * */

    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUserService _userService;

        public UserController(ILogger<UserController> logger, IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        [HttpGet]
        [Route("api/v1/users")]
        public ActionResult<IEnumerable<UserDTO>> GetUsers([FromQuery] UserType? type)
        {
            try
            {
                return Ok(_userService.GetUsers(type));
            }
            catch (Exception e)
            {
                return ErrorHandling.CreateUnexpectedError(e);
            }
            
        }
        [HttpGet]
        [Route("api/v1/users/{id}")]
        public ActionResult<UserDTO> GetUser(Guid id)
        {
            try
            {
                return Ok(_userService.GetUserById(id));
            }
            catch (ResourceNotFoundException e)
            {
                return ErrorHandling.CreateNotFoundError(e);
            }
            catch (Exception e)
            {
                return ErrorHandling.CreateUnexpectedError(e);
            }
        }
        [HttpPost]
        [Route("api/v1/users")]
        public ActionResult<UserDTO> AddUser(UserRequest request)
        {
            try
            {
                return Ok(_userService.AddUser(request));
            }
            catch (Exception e)
            {
                return ErrorHandling.CreateUnexpectedError(e);
            }
           
        }
        [HttpPut]
        [Route("api/v1/users/{id}")]
        public ActionResult<UserDTO> UpdateUser(Guid id, UserDTO user)
        {
            try
            {
                return Ok(_userService.UpdateUser(id,user));
            }
            catch (ResourceNotFoundException e)
            {
                return ErrorHandling.CreateNotFoundError(e);
            }
            catch (Exception e)
            {
                return ErrorHandling.CreateUnexpectedError(e);
            }
        }
        [HttpDelete]
        [Route("api/v1/users/{id}")]
        public ActionResult<UserDTO> DeleteUser(Guid id)
        {
            try
            {
                _userService.DeleteUser(id);
                return Ok();
            }
            catch (ResourceNotFoundException e)
            {
                return ErrorHandling.CreateNotFoundError(e);
            }
            catch (Exception e)
            {
                return ErrorHandling.CreateUnexpectedError(e);
            }
        }
    }
}
