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
        public ActionResult<IEnumerable<TransactionDTO>> GetTransactions([FromQuery] Guid clientId)
        {
            NotEmptyGuidValidator.Run(clientId);
            return Ok(_transactionService.GetTransactions(clientId));
        }

        /// <summary>
        /// Get a single Transaction.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}")]
        public ActionResult<TransactionDTO> GetTransaction([FromQuery] Guid id)
        {
            NotEmptyGuidValidator.Run(id);
            return Ok(_transactionService.GetTransactionById(id));
        }

        /// <summary>
        /// Add a new Transaction.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("")]
        public ActionResult<TransactionDTO> AddTransaction(TransactionRequest request)
        {
            AddTransactionValidator.Run(request);
            var transaction = _transactionService.AddTransaction(request);
            return CreatedAtAction(nameof(GetTransaction), transaction);
        }

        /// <summary>
        /// Delete an existing Transaction.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{id}")]
        public ActionResult<TransactionDTO> DeleteTransaction(Guid id)
        {
            NotEmptyGuidValidator.Run(id);
            _transactionService.DeleteTransaction(id);
            return Ok();
        }
    }
}
