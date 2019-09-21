using System.Threading;
using System.Threading.Tasks;
using ArayeTestProject.Api.Application.Models.Domain;
using ArayeTestProject.Api.Application.Models.Sales;
using ArayeTestProject.Api.Presistences.Exceptions;
using ArayeTestProject.Api.Presistences.IRepositories;
using AutoMapper;
using MediatR;

namespace ArayeTestProject.Api.Application.Commands {
    public class AddSaleCommandHandler : IRequestHandler<AddSaleCommand> {
        private readonly IMainRepository repository;
        private readonly IMapper mapper;

        public AddSaleCommandHandler (IMainRepository repository, IMapper mapper) {
            this.repository = repository;
            this.mapper = mapper;
        }
        public async Task<Unit> Handle (AddSaleCommand request, CancellationToken cancellationToken) {
            if (!(await repository.IsProductIdExist (request.Resource.ProductId)))
                throw new ProductNameNotFoundException ();

            if (!(await repository.IsProductNameExist (request.Resource.ProductName)))
                throw new ProductNameNotFoundException ();

            if (!(await repository.IsUsertNameExist (request.Resource.UserName)))
                throw new UserNameNotFoundException ();

            var lastPrice = await repository.GetLastSalePrice (mapper.Map<SaleListFilterModel> (request.Resource));
            if (lastPrice + (lastPrice * 0.15) < request.Resource.Price)
                throw new MaximumAmountThresholdException ();

            var sale = mapper.Map<SaleResource, Sale> (request.Resource);

            sale.CityId = await repository.IsCityNameExist (request.Resource.CityName);

            await repository.AddSale (sale);

            return Unit.Value;
        }
    }
}