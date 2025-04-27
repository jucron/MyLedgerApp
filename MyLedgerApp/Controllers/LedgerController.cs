using Microsoft.AspNetCore.Mvc;
using MyLedgerApp.Api.v1.Models;
using MyLedgerApp.Services;
using MyLedgerApp.Utils;
using static MyLedgerApp.Utils.Exceptions;

namespace MyLedgerApp.Controllers
{
    /*
     * endpoints:
     * - GET api/v1/ledgers
     * - GET api/v1/ledgers/{id}
     * - POST api/v1/ledgers
     * - PUT api/v1/ledgers/{id}
     * - DELETE api/v1/ledgers/{id}
     * */

    [ApiController]
    [Route("[controller]")]
    public class LedgerController : ControllerBase
    {
        private readonly ILogger<LedgerController> _logger;
        private readonly ILedgerService _ledgerService;

        public LedgerController(ILogger<LedgerController> logger, ILedgerService ledgerService)
        {
            _logger = logger;
            _ledgerService = ledgerService;
        }

        [HttpGet]
        [Route("api/v1/ledgers")]
        public ActionResult<IEnumerable<LedgerDTO>> GetLedgers([FromQuery] bool isFullResponse)
        {
            try
            {
                return Ok(_ledgerService.GetAllLedgers(isFullResponse));
            }
            catch (Exception e)
            {
                return ErrorHandling.CreateUnexpectedError(e);
            }
            
        }
        [HttpGet]
        [Route("api/v1/ledgers/{id}")]
        public ActionResult<LedgerDTO> GetLedger([FromQuery] bool isFullResponse, Guid id)
        {
            try
            {
                return Ok(_ledgerService.GetLedgerById(id, isFullResponse));
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
        [Route("api/v1/ledgers")]
        public ActionResult<LedgerDTO> AddLedger(LedgerRequest user)
        {
            try
            {
                return Ok(_ledgerService.AddLedger(user));
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
        [Route("api/v1/ledgers/{id}")]
        public ActionResult<LedgerDTO> DeleteLedger(Guid id)
        {
            try
            {
                _ledgerService.DeleteLedger(id);
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
