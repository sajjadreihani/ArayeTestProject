using System;
using System.Threading.Tasks;
using ArayeTestProject.Api.Application.Commands;
using ArayeTestProject.Api.Application.Models.Domain;
using ArayeTestProject.Api.Application.Models.Sales;
using ArayeTestProject.Api.Presistences.Exceptions;
using ArayeTestProject.Api.Presistences.IRepositories;
using AutoMapper;
using Moq;
using Xunit;

namespace ArayeTestProject.Test.Application.Command {
    public class RemoveSaleCommandHandlerTest {
        public RemoveSaleCommand request { get; set; }
        public RemoveSaleCommandHandlerTest () {
            request = new RemoveSaleCommand (new SaleResource () {
                Id = 2
            });
        }

        [Fact]
        public async Task RemoveSale_Test_With_Invalid_SaleId () {
            try {
                var repositoryMock = new Mock<IMainRepository> ();
                repositoryMock.Setup (rm => rm.DeleteSale (It.IsAny<long> ())).ThrowsAsync (new SaleNotFoundException ());

                RemoveSaleCommandHandler handler = new RemoveSaleCommandHandler (repositoryMock.Object);
                await Assert.ThrowsAsync<SaleNotFoundException> (async () => await handler.Handle (request, default));

            } catch (Exception ex) {
                Assert.False (true, ex.Message);
            }

        }

        [Fact]
        public async Task RemoveSale_Test_With_Valid_Data () {
            try {
                var repositoryMock = new Mock<IMainRepository> ();
                repositoryMock.Setup (rm => rm.DeleteSale (It.IsAny<long> ()));

                RemoveSaleCommandHandler handler = new RemoveSaleCommandHandler (repositoryMock.Object);
                var result = await handler.Handle (request, default);

                Assert.NotNull (result);

            } catch (Exception ex) {
                Assert.False (true, ex.Message);
            }

        }

    }
}