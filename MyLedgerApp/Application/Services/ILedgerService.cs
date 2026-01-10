using MyLedgerApp.Api.v1.Models;

namespace MyLedgerApp.Application.Services
{
    public interface ILedgerService
    {
        LedgerDTO AddLedger(LedgerRequest request);
        void DeleteLedger(Guid id);
        LedgerDTO GetLedgerById(Guid id, bool isFullResponse);
        IEnumerable<LedgerDTO> GetAllLedgers(bool isFullResponse);
    }
}
