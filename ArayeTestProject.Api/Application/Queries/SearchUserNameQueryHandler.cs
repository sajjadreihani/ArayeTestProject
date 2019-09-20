using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ArayeTestProject.Api.Presistences.IRepositories;
using MediatR;

namespace ArayeTestProject.Api.Application.Queries {
    public class SearchUserNameQueryHandler : IRequestHandler<SearchUserNameQuery, List<string>> {
        private readonly IMainRepository repository;

        public SearchUserNameQueryHandler (IMainRepository repository) {
            this.repository = repository;
        }

        public async Task<List<string>> Handle (SearchUserNameQuery request, CancellationToken cancellationToken) {
            return await repository.GetNames (request.SearchKey);
        }
    }
}