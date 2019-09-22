using System.Threading;
using System.Threading.Tasks;
using ArayeTestProject.Api.Application.Models.Domain;
using ArayeTestProject.Api.Application.Models.Sales;
using ArayeTestProject.Api.Presistences.Exceptions;
using ArayeTestProject.Api.Presistences.IRepositories;
using AutoMapper;
using MediatR;

namespace ArayeTestProject.Api.Application.Commands {
    public class UpdateSaleCommandHandler : IRequestHandler<UpdateSaleCommand> {
        private readonly IMainRepository repository;
        private readonly IMapper mapper;

        public UpdateSaleCommandHandler (IMainRepository repository, IMapper mapper) {
            this.repository = repository;
            this.mapper = mapper;
        }
        public async Task<Unit> Handle (UpdateSaleCommand request, CancellationToken cancellationToken) {
            if (!(await repository.IsProductExist (request.Resource.ProductId,request.Resource.ProductName)))
                throw new ProductNameNotFoundException ();

            if (!(await repository.IsUsertNameExist (request.Resource.UserName)))
                throw new UserNameNotFoundException ();

            var sale = mapper.Map<SaleResource, Sale> (request.Resource);

            sale.CityId = await repository.IsCityNameExist (request.Resource.CityName);
            sale.Id = request.Resource.Id;
            await repository.UpdateSale (sale);

            return Unit.Value;
        }
    }
}