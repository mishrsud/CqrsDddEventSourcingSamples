using MediatR;

namespace Domain
{
    public interface ICommandHandler<in TRequest> : IAsyncRequestHandler<TRequest> where TRequest : ICommand
    {
    }
}
