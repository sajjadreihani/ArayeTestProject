using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ArayeTestProject.Api.Application.Models.Sales;
using ArayeTestProject.Api.Presistences.IRepositories;
using AutoMapper;
using MediatR;

namespace ArayeTestProject.Api.Application.Queries {
    public class GetSaleListQueryHandler : IRequestHandler<GetSaleListQuery, List<SaleResource>> {
        private readonly IMainRepository repository;
        private readonly IMapper mapper;

        public GetSaleListQueryHandler (IMainRepository repository, IMapper mapper) {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<List<SaleResource>> Handle (GetSaleListQuery request, CancellationToken cancellationToken) {
            return mapper.Map<List<Models.Domain.Sale>, List<SaleResource>> (await repository.GetSaleList (request.Filter));
        }
    }
}