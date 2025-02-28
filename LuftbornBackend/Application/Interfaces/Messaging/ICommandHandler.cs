using Domain.Shared;
using Application.Interfaces.Messaging;
using MediatR;

namespace ApexCoach.Application.Interfaces.Messaging;

public interface ICommandHandler<TCommand> : IRequestHandler<TCommand, Result>
    where TCommand : ICommand
{
}

public interface ICommandHandler<TCommand, TResponse> : IRequestHandler<TCommand, Result<TResponse>>
where TCommand : ICommand<TResponse>
{
}