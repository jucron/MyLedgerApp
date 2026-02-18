using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyLedgerApp.Api.v1.Models;
using MyLedgerApp.Application.Services;
using MyLedgerApp.Application.Services.Transactions;
using MyLedgerApp.Application.Validation;
using MyLedgerApp.Application.Validation.User;

namespace MyLedgerApp.Api.v1.Controllers
{
    [ApiController]
    [Route("api/v1/clients")]
    [Authorize]
    public class ClientController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ITransactionService _transactionService;

        public ClientController(IUserService userService, ITransactionService transactionService)
        {
            _userService = userService;
            _transactionService = transactionService;
        }

        /// <summary>
        /// Get all Clients.
        /// </summary>
        /// <returns>List of Clients</returns>
        [HttpGet]
        [Route("")]
        public async Task<ActionResult<IEnumerable<ClientDTO>>> GetClients()
        {
            return Ok(await _userService.GetClients());
        }

        /// <summary>
        /// Get a single Client.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Client DTO</returns>
        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<ClientDTO>> GetClient(Guid id)
        {
            GuidValidator.Run(id);
            return Ok(await _userService.GetClient(id));
        }

        /// <summary>
        /// [OPEN] Add a single Client.
        /// </summary>
        /// <param name="request"></param>
        /// <returns>Client DTO</returns>
        [HttpPost]
        [Route("")]
        [AllowAnonymous]
        public async Task<ActionResult<ClientDTO>> AddClient(AddClientRequest request)
        {
            AddClientValidator.Run(request);
            var user = await _userService.AddClient(request);
            return CreatedAtAction(nameof(GetClient), new {id = user.Id }, user);
        }

        /// <summary>
        /// Update a single Client. 
        /// Note that User's Credentials cannot be updated here.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="user"></param>
        /// <returns>Client DTO</returns>
        [HttpPut]
        [Route("{clientId}")]
        public async Task<ActionResult<ClientDTO>> UpdateClient(Guid id, UpdateClientRequest user)
        {
            GuidValidator.Run(id);
            UpdateClientValidator.Run(user);
            return Ok(await _userService.UpdateClient(id, user));
        }

        /// <summary>
        /// Delete a single Client.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Nothing</returns>
        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult> DeleteClient(Guid id)
        {
            GuidValidator.Run(id);
            await _userService.DeleteClient(id);
            return NoContent();
        }

        /// <summary>
        /// Get all transactions, belonging to a Client.
        /// </summary>
        /// <param name="clientId"></param>
        /// <returns>List of Transactions DTOs</returns>
        [HttpGet]
        [Route("{clientId}/transactions")]
        public async Task<ActionResult<IEnumerable<TransactionDTO>>> GetTransactions(Guid clientId)
        {
            GuidValidator.Run(clientId);
            return Ok(await _transactionService.GetTransactionsByClient(clientId));
        }
    }
}
