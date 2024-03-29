using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ArayeTestProject.Api.Application.Models.Domain;
using ArayeTestProject.Api.Presistences.IRepositories;
using MediatR;

namespace ArayeTestProject.Api.Application.Queries {
    public class SearchCityNameQueryHandler : IRequestHandler<SearchCityNameQuery, List<City>> {
        private readonly IMainRepository repository;

        public SearchCityNameQueryHandler (IMainRepository repository) {
            this.repository = repository;
        }

        public async Task<List<City>> Handle (SearchCityNameQuery request, CancellationToken cancellationToken) {
            return await repository.GetCityNames (request.SearchKey);
        }
    }
}