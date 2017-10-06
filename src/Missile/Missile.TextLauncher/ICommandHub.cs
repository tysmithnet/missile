using System;

namespace Missile.TextLauncher
{
    public interface ICommandHub
    {
        void Broadcast(ICommand command);
        IObservable<TCommand> Get<TCommand>() where TCommand : ICommand;
    }
}