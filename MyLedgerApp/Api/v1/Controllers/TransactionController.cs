using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyLedgerApp.Api.v1.Models;
using MyLedgerApp.Application.Services.Transactions;
using MyLedgerApp.Application.Validation;
using MyLedgerApp.Application.Validation.User;

namespace MyLedgerApp.Api.v1.Controllers
{
    [ApiController]
    [Route("api/v1/transactions")]
    [Authorize]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService _transactionService;

        public TransactionController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        /// <summary>
        /// Get all transactions, belonging to a Client.
        /// </summary>
        /// <param name="clientId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("")]
        public async Task<ActionResult<IEnumerable<TransactionDTO>>> GetTransactions([FromQuery] Guid clientId, CancellationToken ct)
        {
            NotEmptyGuidValidator.Run(clientId);
            return Ok(await _transactionService.GetTransactions(clientId, ct));
        }

        /// <summary>
        /// Get a single Transaction.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<TransactionDTO>> GetTransaction([FromQuery] Guid id, CancellationToken ct)
        {
            NotEmptyGuidValidator.Run(id);
            return Ok(await _transactionService.GetTransactionById(id, ct));
        }

        /// <summary>
        /// Add a new Transaction.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("")]
        public async Task<ActionResult<TransactionDTO>> AddTransaction(TransactionRequest request, CancellationToken ct)
        {
            AddTransactionValidator.Run(request);
            var transaction = await _transactionService.AddTransaction(request, ct);
            return CreatedAtAction(nameof(GetTransaction), transaction);
        }

        /// <summary>
        /// Delete an existing Transaction.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult<TransactionDTO>> DeleteTransaction(Guid id, CancellationToken ct)
        {
            NotEmptyGuidValidator.Run(id);
            await _transactionService.DeleteTransaction(id, ct);
            return Ok();
        }
    }
}
