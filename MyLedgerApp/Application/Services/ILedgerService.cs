using MyLedgerApp.Api.v1.Models;

namespace MyLedgerApp.Application.Services
{
    public interface ILedgerService
    {
        Task<LedgerDTO> AddLedger(LedgerRequest request, CancellationToken cancellationToken);
        Task DeleteLedger(Guid id, CancellationToken cancellationToken);
        Task<LedgerDTO> GetLedgerById(Guid id, bool isFullResponse, CancellationToken cancellationToken);
        Task<IEnumerable<LedgerDTO>> GetAllLedgers(bool isFullResponse, CancellationToken cancellationToken);
    }
}
