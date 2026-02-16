using MyLedgerApp.Domain.Entities;
using MyLedgerApp.Domain.Entities.Users;
using MyLedgerApp.Infrastructure.DbConfig;

namespace Tests.Integration.Transactions
{
    public class TransactionTestCaseHelper
    {
        public Client Client { get; private set; } = null!;
        public Employee Employee { get; private set; } = null!;
        public Ledger Ledger { get; private set; } = null!;

        private readonly AppDbContext _db;

        public TransactionTestCaseHelper(AppDbContext db)
        {
            _db = db;
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
    }
}
