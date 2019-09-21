using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ArayeTestProject.Api.Application.Models.Sales;
using ArayeTestProject.Api.Presistences.IRepositories;
using AutoMapper;
using MediatR;

namespace ArayeTestProject.Api.Application.Queries {
    public class SearchProductNameQueryHandler : IRequestHandler<SearchProductNameQuery, List<ProductResource>> {
        private readonly IMainRepository repository;
        private readonly IMapper mapper;

        public SearchProductNameQueryHandler (IMainRepository repository, IMapper mapper) {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<List<ProductResource>> Handle (SearchProductNameQuery request, CancellationToken cancellationToken) {
            return mapper.Map<List<Models.Domain.Sale>, List<ProductResource>> (await repository.GetProductNames (request.SearchKey));
        }
    }
}