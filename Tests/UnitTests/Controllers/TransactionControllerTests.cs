using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Moq;
using MyLedgerApp.Api.v1.Controllers;
using MyLedgerApp.Api.v1.Models;
using MyLedgerApp.Application.Services.Transactions;

namespace Tests.UnitTests.Controllers
{
    public class TransactionControllerTests
    {

        private readonly Mock<ITransactionService> _serviceMock;
        private readonly TransactionController _controller;

        public TransactionControllerTests()
        {
            _serviceMock = new Mock<ITransactionService>();
            _controller = new TransactionController(_serviceMock.Object);
        }

        [Fact]
        public async Task GetTransactions_ReturnsOk_WithTransactions()
        {

            // given //
            var clientId = Guid.NewGuid();
            var transactions = new List<TransactionDTO>
                {
                    new() { Id = Guid.NewGuid() }
                };

            _serviceMock
                .Setup(s => s.GetTransactions(clientId))
                .ReturnsAsync(transactions);

            // when //
            var result = await _controller.GetTransactions(clientId);

            // then //
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedData = Assert.IsAssignableFrom<IEnumerable<TransactionDTO>>(okResult.Value);

            Assert.Single(returnedData);
            _serviceMock.Verify(s => s.GetTransactions(clientId), Times.Once);
        }

        [Fact]
        public async Task GetTransaction_ReturnsOk_WithTransaction()
        {
            // given //
            var id = Guid.NewGuid();
            var transaction = new TransactionDTO { Id = id };

            _serviceMock
                .Setup(s => s.GetTransactionById(id))
                .ReturnsAsync(transaction);

            // when //
            var result = await _controller.GetTransaction(id);


            // then //
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returned = Assert.IsType<TransactionDTO>(okResult.Value);

            Assert.Equal(id, returned.Id);
            _serviceMock.Verify(s => s.GetTransactionById(id), Times.Once);
        }

        [Fact]
        public async Task AddTransaction_ReturnsCreatedAtAction()
        {
            // given //
            var request = new TransactionRequest { LedgerId = Guid.NewGuid(), Amount = 100, Description = "test"  };
            var createdTransaction = new TransactionDTO { Id = Guid.NewGuid() };

            _serviceMock
                .Setup(s => s.AddTransaction(request))
                .ReturnsAsync(createdTransaction);

            // when //
            var result = await _controller.AddTransaction(request);

            // then //
            var createdResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            var returned = Assert.IsType<TransactionDTO>(createdResult.Value);

            Assert.Equal(nameof(TransactionController.GetTransaction), createdResult.ActionName);
            Assert.Equal(createdTransaction.Id, returned.Id);

            _serviceMock.Verify(s => s.AddTransaction(request), Times.Once);
        }

        [Fact]
        public async Task DeleteTransaction_ReturnsNoContent()
        {
            // given //
            var id = Guid.NewGuid();

            _serviceMock
                .Setup(s => s.DeleteTransaction(id))
                .Returns(Task.CompletedTask);

            // when //
            var result = await _controller.DeleteTransaction(id);

            // then //
            Assert.IsType<NoContentResult>(result);
            _serviceMock.Verify(s => s.DeleteTransaction(id), Times.Once);
        }

        [Fact]
        public async Task GetTransaction_InvalidGuid_ThrowsException()
        {
            await Assert.ThrowsAsync<ValidationException>(() =>
                _controller.GetTransaction(Guid.Empty)
            );
        }



    }
}
