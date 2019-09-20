using System.Collections.Generic;
using MediatR;

namespace ArayeTestProject.Api.Application.Queries {
    public class SearchUserNameQuery : IRequest<List<string>> {
        public SearchUserNameQuery (string searchKey) {
            SearchKey = searchKey;
        }

        public string SearchKey { get; }
    }
}