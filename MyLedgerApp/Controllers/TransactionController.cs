using Microsoft.AspNetCore.Mvc;
using MyLedgerApp.Api.v1.Models;
using MyLedgerApp.Services;
using MyLedgerApp.Utils;
using static MyLedgerApp.Utils.Exceptions;

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
            try
            {
                return Ok(_transactionService.GetAllTransactions());
            }
            catch (Exception e)
            {
                return ErrorHandling.CreateUnexpectedError(e);
            }

        }
        [HttpGet]
        [Route("api/v1/transactions/{id}")]
        public ActionResult<TransactionDTO> GetTransaction(Guid id)
        {
            try
            {
                return Ok(_transactionService.GetTransactionById(id));
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
        [Route("api/v1/transactions")]
        public ActionResult<TransactionDTO> AddTransaction(TransactionRequest request)
        {
            try
            {
                return Ok(_transactionService.AddTransaction(request));
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
        [Route("api/v1/transactions/{id}")]
        public ActionResult<TransactionDTO> DeleteTransaction(Guid id)
        {
            try
            {
                _transactionService.DeleteTransaction(id);
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
