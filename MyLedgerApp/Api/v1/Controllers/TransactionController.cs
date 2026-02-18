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
        /// Get a single Transaction.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<TransactionDTO>> GetTransaction(Guid id)
        {
            GuidValidator.Run(id);
            return Ok(await _transactionService.GetTransactionById(id));
        }

        /// <summary>
        /// Add a new Transaction.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("")]
        public async Task<ActionResult<TransactionDTO>> AddTransaction(TransactionRequest request)
        {
            AddTransactionValidator.Run(request);
            var transaction = await _transactionService.AddTransaction(request);
            return CreatedAtAction(nameof(GetTransaction), new { id = transaction.Id },  transaction);
        }

        /// <summary>
        /// Delete an existing Transaction.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult> DeleteTransaction(Guid id)
        {
            GuidValidator.Run(id);
            await _transactionService.DeleteTransaction(id);
            return NoContent();
        }
    }
}
