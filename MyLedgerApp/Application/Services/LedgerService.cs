using MyLedgerApp.Api.v1.Mappers;
using MyLedgerApp.Api.v1.Models;
using MyLedgerApp.Domain.Entities;
using MyLedgerApp.Infrastructure.Repositories;
using static MyLedgerApp.Common.Utils.Exceptions;

namespace MyLedgerApp.Application.Services
{
    public class LedgerService : ILedgerService
    {
        private readonly ILedgerRepository _ledgerRepository;
        private readonly IUserRepository _userRepository;
        public LedgerService(ILedgerRepository ledgerRepository, IUserRepository userRepository)
        {
            _ledgerRepository = ledgerRepository;
            _userRepository = userRepository;
        }

        public async Task<LedgerDTO> AddLedger(LedgerRequest request, CancellationToken ct)
        {
            var clientOwner = await _userRepository.GetUserById(request.ClientId, ct) ??
                throw new UserNotFoundException(request.ClientId);

            var employee = await _userRepository.GetUserById(request.EmployeeId, ct) ??
                throw new UserNotFoundException(request.EmployeeId);

            if (clientOwner is not Client)
                throw new InvalidOperationException($"User {clientOwner.Name} should be a Client");

            if (employee is not Employee)
                throw new InvalidOperationException($"User {employee.Name} should be an Employee");

            Ledger ledgerToBeAdded = new() { ClientId = clientOwner.Id, EmployeeId = employee.Id };

            await _ledgerRepository.AddLedger(ledgerToBeAdded, ct);
            return LedgerMapper.MapLedgerToLedgerDTO(ledgerToBeAdded);
        }

        public async Task DeleteLedger(Guid id, CancellationToken ct)
        {
            var ledgerToDelete = await _ledgerRepository.GetLedgerById(id, includeTransactions: false, ct) ??
                throw new LedgerNotFoundException(id);

            await _ledgerRepository.DeleteLedger(ledgerToDelete, ct);
        }

        public async Task<LedgerDTO> GetLedgerById(Guid id, bool includeTransactions, CancellationToken ct)
        {
            var ledgerToReturn = await _ledgerRepository.GetLedgerById(id, includeTransactions, ct) ??
                throw new LedgerNotFoundException(id);

            return LedgerMapper.MapLedgerToLedgerDTO(ledgerToReturn);
        }

        public async Task<IEnumerable<LedgerDTO>> GetAllLedgers(bool includeTransactions, CancellationToken ct)
        {
            var list = await _ledgerRepository.GetAllLedgers(includeTransactions,ct);
            return list.Select(l => LedgerMapper.MapLedgerToLedgerDTO(l));
        }


     
    }
}