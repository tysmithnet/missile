using System;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Primitives;
using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace Missile.TextLauncher
{
    [Export(typeof(ICommandHub))]
    public class CommandHub : ICommandHub
    {
        protected internal Subject<ICommand> Source { get; set; } = new Subject<ICommand>();

        public void Broadcast(ICommand command)
        {
            Source.OnNext(command);
        }

        public IObservable<TCommand> Get<TCommand>() where TCommand : ICommand
        {
            return Source.Where(x => x is TCommand).Cast<TCommand>();
        }
    }
}