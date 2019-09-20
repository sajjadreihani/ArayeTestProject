using System.Collections.Generic;
using MediatR;

namespace ArayeTestProject.Api.Application.Queries {
    public class SearchCityNameQuery : IRequest<List<string>> {
        public SearchCityNameQuery (string searchKey) {
            SearchKey = searchKey;
        }

        public string SearchKey { get; }
    }
}