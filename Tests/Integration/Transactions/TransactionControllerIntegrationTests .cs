using System.Net;
using System.Net.Http.Json;
using Microsoft.Extensions.DependencyInjection;
using MyLedgerApp.Domain.Entities;
using MyLedgerApp.Infrastructure.DbConfig;

namespace Tests.Integration.Transactions
{
    [Collection(TestApiCollection.IntegrationTestsName)]
    public class TransactionControllerIntegrationTests
    {
        private readonly AppDbContext _appDbContext;
        private readonly HttpClient _client;
        private readonly TransactionTestCaseHelper _testCaseHelper;

        public TransactionControllerIntegrationTests(IntegrationTestsFactory factory)
        {
            _client = factory.CreateClient();
            factory.InitializeDatabase();
            _appDbContext = factory.Services.CreateScope().ServiceProvider.GetRequiredService<AppDbContext>();
            _testCaseHelper = new TransactionTestCaseHelper(_appDbContext);
        }

        [Fact]
        public async Task GetTransaction_Returns200()
        {

            // GIVEN //
            await _testCaseHelper.InitTestCase();
            Transaction transaction = new()
            {
                Description = "test",
                Amount = 100,
                Type = TransactionType.Deposit,
                LedgerId = _testCaseHelper.Ledger.Id
            };

            await _testCaseHelper.AddTransaction(transaction);

            var reqUri = $"/api/v1/transactions/{transaction.Id}";

            // WHEN //
            TestAuthHandler.IsAuthEnabled = true;
            var unauthorizedResponse = await _client.GetAsync(reqUri);

            TestAuthHandler.IsAuthEnabled = false;
            var response = await _client.GetAsync(reqUri);

            var transactionParsed = await response.Content.ReadFromJsonAsync<TransactionDTOExposed>();

            // THEN //
            Assert.Equal(HttpStatusCode.Unauthorized, unauthorizedResponse.StatusCode);

            response.EnsureSuccessStatusCode();
            Assert.NotNull(transactionParsed);
            Assert.Equal(transaction.Id, transactionParsed.Id);
            Assert.Equal(transaction.Type, Enum.Parse<TransactionType>(transactionParsed.Type));
            Assert.Equal(transaction.Amount, transactionParsed.Amount);
            Assert.Equal(transaction.Description, transactionParsed.Description);
        }
        /*
        [Fact]
        public async Task GetTransactions_Returns200()
        {
            var clientId = Guid.NewGuid();
            var reqUri = $"/api/v1/transactions?clientId={clientId}";

            var response = await _client.GetAsync(reqUri);

            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task DeleteTransaction_Returns204()
        {
            var id = Guid.NewGuid();
            var reqUri = $"/api/v1/transactions/{id}";

            var response = await _client.DeleteAsync(reqUri);

            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        }
        */
    }

}
