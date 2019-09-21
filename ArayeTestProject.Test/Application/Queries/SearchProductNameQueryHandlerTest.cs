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
    public class SearchProductNameQueryHandlerTest {
        [Fact]
        public async Task Handle_Test_With_Data () {
            try {
                var repositoryMock = new Mock<IMainRepository> ();
                repositoryMock.Setup (rm => rm.GetProductNames (It.IsAny<string> ())).ReturnsAsync (It.IsAny<List<Sale>> ());

                var mockMapper = new Mock<IMapper> ();
                mockMapper.Setup (x => x.Map<List<Sale>, List<ProductResource>> (It.IsAny<List<Sale>> ()))
                    .Returns (GetTestNames ());

                SearchProductNameQueryHandler handler = new SearchProductNameQueryHandler (repositoryMock.Object, mockMapper.Object);
                var handlerResult = await handler.Handle (new SearchProductNameQuery (It.IsAny<string> ()), default);

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
                repositoryMock.Setup (rm => rm.GetProductNames (It.IsAny<string> ())).ReturnsAsync (new List<Sale> ());

                var mockMapper = new Mock<IMapper> ();
                mockMapper.Setup (x => x.Map<List<Sale>, List<ProductResource>> (It.IsAny<List<Sale>> ()))
                    .Returns (new List<ProductResource> ());

                SearchProductNameQueryHandler handler = new SearchProductNameQueryHandler (repositoryMock.Object, mockMapper.Object);
                var handlerResult = await handler.Handle (new SearchProductNameQuery (It.IsAny<string> ()), default);

                Assert.Empty (handlerResult);

            } catch (Exception ex) {
                Assert.False (true, ex.Message);
            }
        }

        private List<ProductResource> GetTestNames () {
            return new List<ProductResource> () {
                new ProductResource () {
                        ProductId = "1"
                    },
                    new ProductResource () {
                        ProductId = "2"
                    }
            };
        }
    }
}