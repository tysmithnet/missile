using System;
using System.ComponentModel.Composition;
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

        public Task Interpret(RootNode rootNode)
        {
            var provider = ProviderRepository.Get(rootNode.ProviderNode.Name);

            if (rootNode.DestinationNode == null)
                rootNode.DestinationNode = new DestinationNode(new DestinationToken("noop"));

            var destination =
                DestinationRepository.Get(rootNode.DestinationNode.Name);

            var providerResult = provider.Provide();
            if (!destination.SourceType.IsAssignableFrom(provider.DestinationType))
            {
                // converter repo get
            }
            

            var destinationTask = destination.Process(provider.Provide());
            return destinationTask;
        }
    }
}