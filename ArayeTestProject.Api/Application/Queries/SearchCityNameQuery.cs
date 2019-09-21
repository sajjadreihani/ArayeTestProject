using System.Collections.Generic;
using ArayeTestProject.Api.Application.Models.Domain;
using MediatR;

namespace ArayeTestProject.Api.Application.Queries {
    public class SearchCityNameQuery : IRequest<List<City>> {
        public SearchCityNameQuery (string searchKey) {
            SearchKey = searchKey;
        }
        public string SearchKey { get; }
    }
}