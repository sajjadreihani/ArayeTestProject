using System.Threading;
using System.Threading.Tasks;
using ArayeTestProject.Api.Presistences.IRepositories;
using MediatR;

namespace ArayeTestProject.Api.Application.Commands {
    public class RemoveSaleCommandHandler : IRequestHandler<RemoveSaleCommand> {
        private readonly IMainRepository repository;

        public RemoveSaleCommandHandler (IMainRepository repository) {
            this.repository = repository;
        }
        public async Task<Unit> Handle (RemoveSaleCommand request, CancellationToken cancellationToken) {

            await repository.DeleteSale (request.Resource.Id);

            return Unit.Value;
        }
    }
}