using ArayeTestProject.Api.Application.Models.Sales;
using MediatR;

namespace ArayeTestProject.Api.Application.Commands {
    public class RemoveSaleCommand : IRequest {
        public RemoveSaleCommand (SaleResource resource) {
            Resource = resource;
        }

        public SaleResource Resource { get; }
    }
}