using System.Collections.Generic;
using ArayeTestProject.Api.Application.Models.Sales;
using MediatR;

namespace ArayeTestProject.Api.Application.Queries {
    public class SearchProductNameQuery : IRequest<List<ProductResource>> {
        public SearchProductNameQuery (string searchKey) {
            SearchKey = searchKey;
        }

        public string SearchKey { get; }
    }
}