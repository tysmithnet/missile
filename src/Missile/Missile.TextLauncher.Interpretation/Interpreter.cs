using System;
using System.ComponentModel.Composition;
using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;

namespace Missile.TextLauncher.Interpretation
{
    [Export(typeof(IInterpreter))]
    public class Interpreter : IInterpreter
    {
        [Import]
        public IProviderRepository ProviderRepository { get; set; }

        [Import]
        public IFilterRepository FilterRepository { get; set; }

        [Import]
        public IDestinationRepository DestinationRepository { get; set; }

        [Import]
        public IConverterRepository ConverterRepository { get; set; }

        public Task Interpret(RootNode rootNode)
        {
            var provider = ProviderRepository.Get(rootNode.ProviderNode.Name);

            if (rootNode.DestinationNode == null)
                rootNode.DestinationNode = new DestinationNode(new DestinationToken("noop"));

            var destination =
                DestinationRepository.Get(rootNode.DestinationNode.Name);

            var providerResult = provider.Provide();
            var toDestination = providerResult;
            if (!destination.SourceType.IsAssignableFrom(provider.DestinationType))
            {
                var sourceType = typeof(IObservable<>).MakeGenericType(provider.DestinationType);
                var destType = typeof(IObservable<>).MakeGenericType(destination.SourceType);
                var converter = ConverterRepository.Get(sourceType, destType);
                toDestination = converter.Convert(toDestination);
            }

            var destinationTask = destination.Process(toDestination);
            return destinationTask;
        }
    }
}