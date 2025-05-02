using MyLedgerApp.Api.v1.Mappers;
using MyLedgerApp.Api.v1.Models;
using MyLedgerApp.Domain;
using MyLedgerApp.Repositories;
using static MyLedgerApp.Utils.Exceptions;

namespace MyLedgerApp.Services
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

        public LedgerDTO AddLedger(LedgerRequest request)
        {
            var clientOwner = _userRepository.GetUserById(request.ClientId);
            var employee = _userRepository.GetUserById(request.EmployeeId);

            if (clientOwner == null)
                throw new UserNotFoundException(request.ClientId);

            if (employee == null)
                throw new UserNotFoundException(request.EmployeeId);

            if (clientOwner is Employee)
                throw new InvalidOperationException($"User {clientOwner.Name} should be a Client");

            if (employee is Client)
                throw new InvalidOperationException($"User {employee.Name} should be an Employee");

            // Add Client to the Ledger 
            Ledger ledgerToBeAdded = new() { Client = (Client)clientOwner, Employee = (Employee)employee };

            // Add Ledger to the Client
            ((Client)clientOwner).Ledgers.Add(ledgerToBeAdded);

            _ledgerRepository.AddLedger(ledgerToBeAdded);
            return LedgerMapper.MapLedgerToLedgerDTO(ledgerToBeAdded, false);
        }

        public void DeleteLedger(Guid id)
        {
            var ledgerToDelete = _ledgerRepository.GetLedgerById(id) ??
                throw new LedgerNotFoundException(id);

            var clientOwner = _userRepository.GetUserById(ledgerToDelete.Client.Id) ??
                throw new UserNotFoundException(ledgerToDelete.Client.Id);
           
            ((Client)clientOwner).Ledgers?.Remove(ledgerToDelete);
            _ledgerRepository.DeleteLedger(ledgerToDelete);
        }

        public LedgerDTO GetLedgerById(Guid id, bool isFullResponse)
        {
            var ledgerToReturn = _ledgerRepository.GetLedgerById(id) ??
                throw new LedgerNotFoundException(id);

            return LedgerMapper.MapLedgerToLedgerDTO(ledgerToReturn, isFullResponse);
        }

        public IEnumerable<LedgerDTO> GetAllLedgers(bool isFullResponse)
        {
            return _ledgerRepository.GetAllLedgers().Select(l => LedgerMapper.MapLedgerToLedgerDTO(l, isFullResponse));
        }


     
    }
}