using MyLedgerApp.Api.v1.Models;

namespace MyLedgerApp.Application.Services
{
    public interface ILedgerService
    {
        Task<LedgerDTO> AddLedger(LedgerRequest request);
        Task DeleteLedger(Guid id);
        Task<LedgerDTO> GetLedgerById(Guid id, bool isFullResponse);
        Task<IEnumerable<LedgerDTO>> GetAllLedgers(bool isFullResponse);
    }
}
