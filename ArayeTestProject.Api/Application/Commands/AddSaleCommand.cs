using ArayeTestProject.Api.Application.Models.Sales;
using MediatR;

namespace ArayeTestProject.Api.Application.Commands {
    public class AddSaleCommand : IRequest {
        public AddSaleCommand (SaleResource resource) {
            Resource = resource;
        }

        public SaleResource Resource { get; }
    }
}