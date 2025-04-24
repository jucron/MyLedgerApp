using Microsoft.AspNetCore.Mvc;
using MyLedgerApp.Api.v1.Models;
using MyLedgerApp.Domain;
using MyLedgerApp.Services;
using static MyLedgerApp.MyLedgerAppExceptions;

namespace MyLedgerApp.Controllers
{
    /*
     * endpoints:
     * - GET api/v1/transactions
     * - GET api/v1/transactions/{id}
     * - POST api/v1/transactions
     * - PUT api/v1/transactions/{id}
     * - DELETE api/v1/transactions/{id}
     * */

    [ApiController]
    [Route("[controller]")]
    public class TransactionController : ControllerBase
    {
        private readonly ILogger<TransactionController> _logger;
        private readonly ITransactionService _transactionService;

        public TransactionController(ILogger<TransactionController> logger, ITransactionService transactionService)
        {
            _logger = logger;
            _transactionService = transactionService;
        }

        [HttpGet]
        [Route("api/v1/transactions")]
        public ActionResult<IEnumerable<TransactionDTO>> GetTransactions()
        {
            return Ok(_transactionService.GetTransactions());
        }
        [HttpGet]
        [Route("api/v1/transactions/{id}")]
        public ActionResult<TransactionDTO> GetTransaction(Guid id)
        {
            try
            {
                return Ok(_transactionService.GetTransactionById(id));
            }
            catch (TransactionNotFoundException e)
            {
                return NotFound(new { message = e.Message });
            }
            catch (Exception e)
            {
                return StatusCode(500, new { message = "Some unexpected error has occured. ", desc = e.Message });
            }
        }
        [HttpPost]
        [Route("api/v1/transactions")]
        public ActionResult<IEnumerable<TransactionDTO>> AddTransaction(TransactionDTO transaction)
        {
            return Ok(_transactionService.AddTransaction(transaction));
        }
    }
}
