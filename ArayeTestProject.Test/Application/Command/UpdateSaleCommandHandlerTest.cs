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
    public class UpdateSaleCommandHandlerTest {
        public UpdateSaleCommand request { get; set; }
        public UpdateSaleCommandHandlerTest () {
            request = new UpdateSaleCommand (new SaleResource () {
                CityName = "Update",
                    Price = 120,
                    ProductId = "1",
                    ProductName = "Update",
                    UserName = "Update",
                    Id = 2
            });
        }

        [Fact]
        public async Task UpdateSale_Test_With_Invalid_ProductId () {
            try {
                var repositoryMock = new Mock<IMainRepository> ();
                repositoryMock.Setup (rm => rm.IsProductExist (It.IsAny<string> (), It.IsAny<string> ())).ReturnsAsync (false);

                var mockMapper = new Mock<IMapper> ();

                UpdateSaleCommandHandler handler = new UpdateSaleCommandHandler (repositoryMock.Object, mockMapper.Object);
                await Assert.ThrowsAsync<ProductNotFoundException> (async () => await handler.Handle (request, default));

            } catch (Exception ex) {
                Assert.False (true, ex.Message);
            }

        }

        [Fact]
        public async Task UpdateSale_Test_With_Invalid_ProductName () {
            try {
                var repositoryMock = new Mock<IMainRepository> ();
                repositoryMock.Setup (rm => rm.IsProductExist (It.IsAny<string> (), It.IsAny<string> ())).ReturnsAsync (false);
                var mockMapper = new Mock<IMapper> ();

                UpdateSaleCommandHandler handler = new UpdateSaleCommandHandler (repositoryMock.Object, mockMapper.Object);
                await Assert.ThrowsAsync<ProductNotFoundException> (async () => await handler.Handle (request, default));

            } catch (Exception ex) {
                Assert.False (true, ex.Message);
            }

        }

        [Fact]
        public async Task UpdateSale_Test_With_Invalid_UserName () {
            try {
                var repositoryMock = new Mock<IMainRepository> ();
                repositoryMock.Setup (rm => rm.IsProductExist (It.IsAny<string> (), It.IsAny<string> ())).ReturnsAsync (true);
                repositoryMock.Setup (rm => rm.IsUsertNameExist (It.IsAny<string> ())).ReturnsAsync (false);

                var mockMapper = new Mock<IMapper> ();

                UpdateSaleCommandHandler handler = new UpdateSaleCommandHandler (repositoryMock.Object, mockMapper.Object);
                await Assert.ThrowsAsync<UserNameNotFoundException> (async () => await handler.Handle (request, default));

            } catch (Exception ex) {
                Assert.False (true, ex.Message);
            }

        }

        [Fact]
        public async Task UpdateSale_Test_With_Invalid_CityName () {
            try {
                var repositoryMock = new Mock<IMainRepository> ();
                repositoryMock.Setup (rm => rm.IsProductExist (It.IsAny<string> (), It.IsAny<string> ())).ReturnsAsync (true);
                repositoryMock.Setup (rm => rm.IsUsertNameExist (It.IsAny<string> ())).ReturnsAsync (true);
                repositoryMock.Setup (rm => rm.IsCityNameExist (It.IsAny<string> ())).ThrowsAsync (new CityNameNotFoundException ());

                var mockMapper = new Mock<IMapper> ();
                mockMapper.Setup (x => x.Map<SaleResource, Sale> (It.IsAny<SaleResource> ()))
                    .Returns (new Sale ());

                UpdateSaleCommandHandler handler = new UpdateSaleCommandHandler (repositoryMock.Object, mockMapper.Object);
                await Assert.ThrowsAsync<CityNameNotFoundException> (async () => await handler.Handle (request, default));

            } catch (Exception ex) {
                Assert.False (true, ex.Message);
            }

        }

        [Fact]
        public async Task UpdateSale_Test_With_Invalid_SaleId () {
            try {
                var repositoryMock = new Mock<IMainRepository> ();
                repositoryMock.Setup (rm => rm.IsProductExist (It.IsAny<string> (), It.IsAny<string> ())).ReturnsAsync (true);
                repositoryMock.Setup (rm => rm.IsUsertNameExist (It.IsAny<string> ())).ReturnsAsync (true);
                repositoryMock.Setup (rm => rm.IsCityNameExist (It.IsAny<string> ())).ReturnsAsync (10);
                repositoryMock.Setup (rm => rm.UpdateSale (It.IsAny<Sale> ())).ThrowsAsync (new SaleNotFoundException ());

                var mockMapper = new Mock<IMapper> ();
                mockMapper.Setup (x => x.Map<SaleResource, Sale> (It.IsAny<SaleResource> ()))
                    .Returns (new Sale ());
                UpdateSaleCommandHandler handler = new UpdateSaleCommandHandler (repositoryMock.Object, mockMapper.Object);
                await Assert.ThrowsAsync<SaleNotFoundException> (async () => await handler.Handle (request, default));

            } catch (Exception ex) {
                Assert.False (true, ex.Message);
            }

        }

        [Fact]
        public async Task UpdateSale_Test_With_Invalid_Price () {
            try {
                var repositoryMock = new Mock<IMainRepository> ();
                repositoryMock.Setup (rm => rm.IsProductExist (It.IsAny<string> (), It.IsAny<string> ())).ReturnsAsync (true);
                repositoryMock.Setup (rm => rm.IsUsertNameExist (It.IsAny<string> ())).ReturnsAsync (true);
                repositoryMock.Setup (rm => rm.IsCityNameExist (It.IsAny<string> ())).ReturnsAsync (10);
                repositoryMock.Setup (rm => rm.UpdateSale (It.IsAny<Sale> ())).ThrowsAsync (new MaximumAmountThresholdException ());

                var mockMapper = new Mock<IMapper> ();
                mockMapper.Setup (x => x.Map<SaleResource, Sale> (It.IsAny<SaleResource> ()))
                    .Returns (new Sale ());
                UpdateSaleCommandHandler handler = new UpdateSaleCommandHandler (repositoryMock.Object, mockMapper.Object);
                await Assert.ThrowsAsync<MaximumAmountThresholdException> (async () => await handler.Handle (request, default));

            } catch (Exception ex) {
                Assert.False (true, ex.Message);
            }

        }

        [Fact]
        public async Task UpdateSale_Test_With_Valid_Data () {
            try {
                var repositoryMock = new Mock<IMainRepository> ();
                repositoryMock.Setup (rm => rm.IsProductExist (It.IsAny<string> (), It.IsAny<string> ())).ReturnsAsync (true);
                repositoryMock.Setup (rm => rm.IsUsertNameExist (It.IsAny<string> ())).ReturnsAsync (true);
                repositoryMock.Setup (rm => rm.IsCityNameExist (It.IsAny<string> ())).ReturnsAsync (10);
                repositoryMock.Setup (rm => rm.UpdateSale (It.IsAny<Sale> ()));

                var mockMapper = new Mock<IMapper> ();
                mockMapper.Setup (x => x.Map<SaleResource, Sale> (It.IsAny<SaleResource> ()))
                    .Returns (new Sale ());

                UpdateSaleCommandHandler handler = new UpdateSaleCommandHandler (repositoryMock.Object, mockMapper.Object);
                var result = await handler.Handle (request, default);

                Assert.NotNull (result);

            } catch (Exception ex) {
                Assert.False (true, ex.Message);
            }

        }

    }
}