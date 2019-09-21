using ArayeTestProject.Api.Application.Models.Sales;
using MediatR;

namespace ArayeTestProject.Api.Application.Commands {
    public class UpdateSaleCommand : IRequest {
        public UpdateSaleCommand (SaleResource resource) {
            Resource = resource;
        }

        public SaleResource Resource { get; }
    }
}