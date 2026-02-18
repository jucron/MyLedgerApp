using Microsoft.EntityFrameworkCore;
using MyLedgerApp.Domain.Entities;
using MyLedgerApp.Domain.Entities.Users;
using MyLedgerApp.Infrastructure.DbConfig;

namespace Tests.Integration.Transactions
{
    public class TransactionTestCaseHelper
    {
        private Client Client = null!;
        private Employee Employee = null!;
        private Ledger Ledger = null!;

        private readonly AppDbContext _db;

        public TransactionTestCaseHelper(AppDbContext db)
        {
            _db = db;
        }

        public Guid GetLedgerId()
        {
            return Ledger.Id;
        }
        public async Task<decimal> GetLedgerCurrentBalanceAsync()
        {
            await _db.Entry(Ledger).ReloadAsync();
            return Ledger.CurrentBalance;
        }

        public async Task InitTestCase()
        {
            Client = new()
            {
                Name = "client_test",
                Email = "client_test@test.com",
                Credential = new Credential("client_test", "pass")
            };
            _db.Users.Add(Client);

            Employee = new()
            {
                Name = "employee_test",
                Email = "employee_test@test.com",
                Credential = new Credential("employee_test", "pass")
            };
            _db.Users.Add(Employee);

            await _db.SaveChangesAsync();

            Ledger = new()
            {
                ClientId = Client.Id,
                EmployeeId = Employee.Id,
            };
            _db.Ledgers.Add(Ledger);

            await _db.SaveChangesAsync();
        }

        public async Task AddTransaction(Transaction transaction)
        {
            _db.Transactions.Add(transaction);
            await _db.SaveChangesAsync();
        }
        public async Task<int> GetTransactionsCountAsync()
        {
            return await _db.Transactions.CountAsync();
        }
        public async Task<int> GetLedgerCount()
        {
            return await _db.Transactions.CountAsync();
        }
    }
}
