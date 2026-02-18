using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using MyLedgerApp.Domain.Entities.Users;
using MyLedgerApp.Infrastructure.DbConfig;
using Tests.Integration;
using Tests.Integration.Transactions;

namespace Tests.UnitTests.Controllers
{
    public class TransactionControllerTestsFixture : IAsyncLifetime
    {
        public HttpClient Client { get; private set; }
        public TransactionTestCaseHelper TestCaseHelper { get; private set; }

        private readonly AppDbContext _appDbContext;

        public TransactionControllerTestsFixture(IntegrationTestsFactory factory)
        {
            Client = factory.CreateClient();
            factory.InitializeDatabase();
            _appDbContext = factory.Services.CreateScope().ServiceProvider.GetRequiredService<AppDbContext>();
            TestCaseHelper = new TransactionTestCaseHelper(_appDbContext);
        }

        public async Task InitializeAsync()
        {
            await TestCaseHelper.InitTestCase();
        }

        public async Task DisposeAsync()
        {
            await _appDbContext.DisposeAsync();
        }
    }

}
