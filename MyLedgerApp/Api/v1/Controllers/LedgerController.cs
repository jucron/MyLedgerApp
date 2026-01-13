using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyLedgerApp.Api.v1.Models;
using MyLedgerApp.Application.Services;
using MyLedgerApp.Application.Validation;
using MyLedgerApp.Application.Validation.User;

namespace MyLedgerApp.Api.v1.Controllers
{
    [ApiController]
    [Route("api/v1/ledgers")]
    [Authorize]
    public class LedgerController : ControllerBase
    {
        private readonly ILedgerService _ledgerService;

        public LedgerController(ILedgerService ledgerService)
        {
            _ledgerService = ledgerService;
        }

        /// <summary>
        /// Get all Ledgers.
        /// </summary>
        /// <param name="isIncludeTransactions"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("")]
        public ActionResult<IEnumerable<LedgerDTO>> GetLedgers([FromQuery] bool isIncludeTransactions)
        {
            return Ok(_ledgerService.GetAllLedgers(isIncludeTransactions));
        }

        /// <summary>
        /// Get a single Ledger.
        /// </summary>
        /// <param name="isIncludeTransactions"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}")]
        public ActionResult<LedgerDTO> GetLedger([FromQuery] bool isIncludeTransactions, Guid id)
        {
            NotEmptyGuidValidator.Run(id);
            return Ok(_ledgerService.GetLedgerById(id, isIncludeTransactions));
        }

        /// <summary>
        /// Add a new Ledger into the system.
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("")]
        public ActionResult<LedgerDTO> AddLedger(LedgerRequest req)
        {
            AddLedgerValidator.Run(req);
            return Ok(_ledgerService.AddLedger(req));
        }

        /// <summary>
        /// Delete a Ledger from the system.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{id}")]
        public ActionResult<LedgerDTO> DeleteLedger(Guid id)
        {
            NotEmptyGuidValidator.Run(id);
            _ledgerService.DeleteLedger(id);
            return Ok();
        }
    }
}
