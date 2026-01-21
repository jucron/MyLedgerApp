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
        /// <param name="includeTransactions"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("")]
        public async Task<ActionResult<IEnumerable<LedgerDTO>>> GetLedgers([FromQuery] bool includeTransactions, CancellationToken ct)
        {
            return Ok(await _ledgerService.GetAllLedgers(includeTransactions, ct));
        }

        /// <summary>
        /// Get a single Ledger.
        /// </summary>
        /// <param name="includeTransactions"></param>
        /// <param name="id"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<LedgerDTO>> GetLedger([FromQuery] bool includeTransactions, [FromQuery] Guid id, CancellationToken ct)
        {
            NotEmptyGuidValidator.Run(id);
            return Ok(await _ledgerService.GetLedgerById(id, includeTransactions, ct));
        }

        /// <summary>
        /// Add a new Ledger into the system.
        /// </summary>
        /// <param name="req"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("")]
        public async Task<ActionResult<LedgerDTO>> AddLedger(LedgerRequest req, CancellationToken ct)
        {
            AddLedgerValidator.Run(req);
            return Ok(await _ledgerService.AddLedger(req, ct));
        }

        /// <summary>
        /// Delete a Ledger from the system.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult<LedgerDTO>> DeleteLedger([FromQuery] Guid id, CancellationToken ct)
        {
            NotEmptyGuidValidator.Run(id);
            await _ledgerService.DeleteLedger(id, ct);
            return Ok();
        }
    }
}
