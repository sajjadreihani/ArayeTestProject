using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ArayeTestProject.Api.Application.Models.Domain;
using ArayeTestProject.Api.Application.Models.Sales;
using ArayeTestProject.Api.Application.Queries;
using ArayeTestProject.Api.Presistences.IRepositories;
using AutoMapper;
using Moq;
using Xunit;

namespace ArayeTestProject.Test.Application.Queries {
    public class GetSaleListQueryHandlerTest {
        [Fact]
        public async Task Handle_Test_With_Data () {
            try {
                var repositoryMock = new Mock<IMainRepository> ();
                repositoryMock.Setup (rm => rm.GetSaleList (It.IsAny<SaleListFilterModel> ())).ReturnsAsync (It.IsAny<List<Sale>> ());

                var mockMapper = new Mock<IMapper> ();
                mockMapper.Setup (x => x.Map<List<Sale>, List<SaleResource>> (It.IsAny<List<Sale>> ()))
                    .Returns (GetTestSales ());

                GetSaleListQueryHandler handler = new GetSaleListQueryHandler (repositoryMock.Object, mockMapper.Object);
                var handlerResult = await handler.Handle (new GetSaleListQuery (It.IsAny<SaleListFilterModel> ()), default);

                Assert.NotEmpty (handlerResult);

                Assert.Equal (2, handlerResult.Count);
            } catch (Exception ex) {
                Assert.False (true, ex.Message);
            }
        }

        [Fact]
        public async Task Handle_Test_With_No_Data () {
            try {
                var repositoryMock = new Mock<IMainRepository> ();
                repositoryMock.Setup (rm => rm.GetSaleList (It.IsAny<SaleListFilterModel> ())).ReturnsAsync (It.IsAny<List<Sale>> ());

                var mockMapper = new Mock<IMapper> ();
                mockMapper.Setup (x => x.Map<List<Sale>, List<SaleResource>> (It.IsAny<List<Sale>> ()))
                    .Returns (new List<SaleResource> ());

                GetSaleListQueryHandler handler = new GetSaleListQueryHandler (repositoryMock.Object, mockMapper.Object);
                var handlerResult = await handler.Handle (new GetSaleListQuery (It.IsAny<SaleListFilterModel> ()), default);

                Assert.Empty (handlerResult);

            } catch (Exception ex) {
                Assert.False (true, ex.Message);
            }
        }

        private List<SaleResource> GetTestSales () {
            return new List<SaleResource> () {
                new SaleResource () {
                        ProductId = "1"
                    },
                    new SaleResource () {
                        ProductId = "2"
                    }
            };
        }
    }
}