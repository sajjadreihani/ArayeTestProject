using System.Collections.Generic;
using MediatR;

namespace ArayeTestProject.Api.Application.Queries {
    public class SearchProductNameQuery : IRequest<List<string>> {
        public SearchProductNameQuery (string searchKey) {
            SearchKey = searchKey;
        }

        public string SearchKey { get; }
    }
}