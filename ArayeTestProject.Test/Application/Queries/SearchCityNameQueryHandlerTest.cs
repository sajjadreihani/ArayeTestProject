using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ArayeTestProject.Api.Application.Models.Domain;
using ArayeTestProject.Api.Application.Queries;
using ArayeTestProject.Api.Presistences.IRepositories;
using Moq;
using Xunit;

namespace ArayeTestProject.Test.Application.Queries {
    public class SearchCityNameQueryHandlerTest {
        [Fact]
        public async Task Handle_Test_With_Data () {
            try {
                var repositoryMock = new Mock<IMainRepository> ();
                repositoryMock.Setup (rm => rm.GetCityNames (It.IsAny<string> ())).ReturnsAsync (GetTestNames ());

                SearchCityNameQueryHandler handler = new SearchCityNameQueryHandler (repositoryMock.Object);
                var handlerResult = await handler.Handle (new SearchCityNameQuery (It.IsAny<string> ()), default);

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
                repositoryMock.Setup (rm => rm.GetCityNames (It.IsAny<string> ())).ReturnsAsync (new List<City> ());

                SearchCityNameQueryHandler handler = new SearchCityNameQueryHandler (repositoryMock.Object);
                var handlerResult = await handler.Handle (new SearchCityNameQuery (It.IsAny<string> ()), default);

                Assert.Empty (handlerResult);

            } catch (Exception ex) {
                Assert.False (true, ex.Message);
            }
        }

        private List<City> GetTestNames () {
            return new List<City> () {
                new City () {
                        Id = 1,
                            Name = "sadsdasd"
                    },
                    new City () {
                        Id = 2,
                            Name = "adasdasd"
                    }
            };
        }
    }
}