using System.Net;
using System.Net.Http.Json;
using Microsoft.Extensions.DependencyInjection;
using MyLedgerApp.Api.v1.Models;
using MyLedgerApp.Domain.Entities;
using MyLedgerApp.Infrastructure.DbConfig;
using Tests.UnitTests.Controllers;

namespace Tests.Integration.Transactions
{
    [Collection(TestApiCollection.IntegrationTestsName)]
    public class TransactionControllerIntegrationTests: IClassFixture<TransactionControllerTestsFixture>
    {
        private readonly HttpClient _client;
        private readonly TransactionTestCaseHelper _testCaseHelper;

        public TransactionControllerIntegrationTests(TransactionControllerTestsFixture fixture)
        {
            _client = fixture.Client;
            _testCaseHelper = fixture.TestCaseHelper;
        }

        [Fact]
        public async Task GetTransaction_Test()
        {
            // GIVEN //
            Transaction transaction = new()
            {
                Description = "test",
                Amount = 100,
                Type = TransactionType.Deposit,
                LedgerId = _testCaseHelper.GetLedgerId()
            };

            await _testCaseHelper.AddTransaction(transaction);

            var reqUri = $"/api/v1/transactions/{transaction.Id}";

            // WHEN //
            TestAuthHandler.IsAuthEnabled = true;
            var unauthorizedResponse = await _client.GetAsync(reqUri);

            TestAuthHandler.IsAuthEnabled = false;
            var response = await _client.GetAsync(reqUri);

            var transactionParsedFromResponse = await response.Content.ReadFromJsonAsync<TransactionDTOExposed>();

            // THEN //
            Assert.Equal(HttpStatusCode.Unauthorized, unauthorizedResponse.StatusCode);

            response.EnsureSuccessStatusCode();
            Assert.NotNull(transactionParsedFromResponse);
            Assert.Equal(transaction.Id, transactionParsedFromResponse.Id);
            Assert.Equal(transaction.Type, Enum.Parse<TransactionType>(transactionParsedFromResponse.Type));
            Assert.Equal(transaction.Amount, transactionParsedFromResponse.Amount);
            Assert.Equal(transaction.Description, transactionParsedFromResponse.Description);
        }

        [Fact]
        public async Task AddTransaction_Test()
        {
            // GIVEN //
            TransactionRequest transaction = new()
            {
                Description = "test",
                Amount = 150,
                Type = TransactionType.Deposit,
                LedgerId = _testCaseHelper.GetLedgerId()
            };

            var reqUri = $"/api/v1/transactions/";

            // WHEN //
            var transactionsCountBefore = await _testCaseHelper.GetTransactionsCountAsync();
            var ledgerBalanceBefore = await _testCaseHelper.GetLedgerCurrentBalanceAsync();

            TestAuthHandler.IsAuthEnabled = true;
            var unauthorizedResponse = await _client.PostAsJsonAsync(reqUri, transaction);

            TestAuthHandler.IsAuthEnabled = false;
            var response = await _client.PostAsJsonAsync(reqUri, transaction);

            var transactionParsedFromResponse = await response.Content.ReadFromJsonAsync<TransactionDTOExposed>();

            var transactionsCountAfter = await _testCaseHelper.GetTransactionsCountAsync();
            var ledgerBalanceAfter = await _testCaseHelper.GetLedgerCurrentBalanceAsync();

            // THEN //
            Assert.Equal(HttpStatusCode.Unauthorized, unauthorizedResponse.StatusCode);

            response.EnsureSuccessStatusCode();
            Assert.NotNull(transactionParsedFromResponse);
            Assert.False(transactionParsedFromResponse.Id == Guid.Empty);
            Assert.Equal(transaction.Type, Enum.Parse<TransactionType>(transactionParsedFromResponse.Type));
            Assert.Equal(transaction.Amount, transactionParsedFromResponse.Amount);
            Assert.Equal(transaction.Description, transactionParsedFromResponse.Description);
            Assert.Equal(transactionsCountBefore + 1, transactionsCountAfter);
            Assert.Equal(ledgerBalanceBefore + transaction.Amount, ledgerBalanceAfter);
        }

        [Fact]
        public async Task DeleteTransaction_Test()
        {
            // GIVEN //
            Transaction transaction = new()
            {
                Description = "test",
                Amount = 10250,
                Type = TransactionType.Deposit,
                LedgerId = _testCaseHelper.GetLedgerId()
            };

            await _testCaseHelper.AddTransaction(transaction);

            var reqUri = $"/api/v1/transactions/{transaction.Id}";

            // WHEN //
            var transactionsCountBefore = await _testCaseHelper.GetTransactionsCountAsync();
            var ledgerBalanceBefore = await _testCaseHelper.GetLedgerCurrentBalanceAsync();

            TestAuthHandler.IsAuthEnabled = true;
            var unauthorizedResponse = await _client.DeleteAsync(reqUri);

            TestAuthHandler.IsAuthEnabled = false;
            var response = await _client.DeleteAsync(reqUri);

            var transactionsCountAfter = await _testCaseHelper.GetTransactionsCountAsync();
            var ledgerBalanceAfter = await _testCaseHelper.GetLedgerCurrentBalanceAsync();

            // THEN //
            Assert.Equal(HttpStatusCode.Unauthorized, unauthorizedResponse.StatusCode);

            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);

            int expectedTransactionsCountAfter = transactionsCountBefore - 1;
            Assert.Equal(expectedTransactionsCountAfter, transactionsCountAfter);

            decimal expectedLedgerBalanceAfter = ledgerBalanceBefore - transaction.Amount;
            Assert.Equal(expectedLedgerBalanceAfter, ledgerBalanceAfter);
        }

    }

}
