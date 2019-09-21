using System.Collections.Generic;
using ArayeTestProject.Api.Application.Models.Sales;
using MediatR;

namespace ArayeTestProject.Api.Application.Queries {
    public class GetSaleListQuery : IRequest<List<SaleResource>> {
        public GetSaleListQuery (SaleListFilterModel filter) {
            Filter = filter;
        }

        public SaleListFilterModel Filter { get; }
    }
}