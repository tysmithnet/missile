using System;
using System.Linq;
using System.Threading.Tasks;

namespace Missile.TextLauncher.Interpretation
{
    public class Interpreter : IInterpreter
    {
        public IProviderRepository ProviderRepository { get; set; }
        public IFilterRepository FilterRepository { get; set; }
        public IDestinationRepository DestinationRepository { get; set; }

        public Task Interpret(RootNode rootNode)
        {
            var provider = ProviderRepository.RegisteredProviders.Single(x => x.Name == rootNode.ProviderNode.Name);

            if (rootNode.DestinationNode == null)
                return new NoopDestination().ProcessAsync((IObservable<object>) provider.Provide());

            var destination =
                DestinationRepository.RegisteredDestinations.Single(x => x.Name == rootNode.DestinationNode.Name);

            var destinationTask = destination.Process(provider.Provide());
            return destinationTask;
        }
    }
}