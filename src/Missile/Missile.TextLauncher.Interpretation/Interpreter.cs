using System;
using System.ComponentModel.Composition;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Missile.TextLauncher.Conversion;
using Missile.TextLauncher.Destination;
using Missile.TextLauncher.Filtration;
using Missile.TextLauncher.Interpretation.Lexing;
using Missile.TextLauncher.Interpretation.Parsing;
using Missile.TextLauncher.Provision;

namespace Missile.TextLauncher.Interpretation
{
    /// <inheritdoc />
    /// <summary>
    /// Default implementation of IInterpreter
    /// </summary>
    /// <seealso cref="T:Missile.TextLauncher.Interpretation.IInterpreter" />
    [Export(typeof(IInterpreter))]
    public class Interpreter : IInterpreter
    {
        /// <summary>
        /// Gets or sets the provider repository.
        /// </summary>
        /// <value>
        /// The provider repository.
        /// </value>
        [Import]
        protected internal IProviderRepository ProviderRepository { get; set; }

        /// <summary>
        /// Gets or sets the filter repository.
        /// </summary>
        /// <value>
        /// The filter repository.
        /// </value>
        [Import]
        protected internal IFilterRepository FilterRepository { get; set; }

        /// <summary>
        /// Gets or sets the destination repository.
        /// </summary>
        /// <value>
        /// The destination repository.
        /// </value>
        [Import]
        protected internal IDestinationRepository DestinationRepository { get; set; }

        /// <summary>
        /// Gets or sets the converter repository.
        /// </summary>
        /// <value>
        /// The converter repository.
        /// </value>
        [Import]
        protected internal IConverterRepository ConverterRepository { get; set; }

        /// <summary>
        /// Gets or sets the observable inspectors.
        /// </summary>
        /// <value>
        /// The observable inspectors.
        /// </value>
        [ImportMany]
        protected internal IObservableInspector[] ObservableInspectors { get; set; }

        /// <inheritdoc />
        /// <summary>
        /// Interprets the AST asynchronously
        /// </summary>
        /// <param name="rootNode">The root node.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// A Task that when complete will signal the completion of the interpretation
        /// </returns>
        /// <exception cref="T:System.ApplicationException">An inspector was not able to be found for a particular type</exception>
        public Task InterpretAsync(RootNode rootNode, CancellationToken cancellationToken)
        {
            var provider = ProviderRepository.Get(rootNode.ProviderNode.Name);

            if (rootNode.DestinationNode == null)
                rootNode.DestinationNode = new DestinationNode(new DestinationToken("noop", new string[0]));

            var destination =
                DestinationRepository.Get(rootNode.DestinationNode.Name);

            var providerResult = provider.Provide(rootNode.ProviderNode.Args);
            var destinationInput = providerResult;
            var inspector = ObservableInspectors.FirstOrDefault(i => i.CanHandle(destinationInput.GetType()));
            if (inspector == null)
                throw new ApplicationException($"Unable to find an inspector for {destinationInput.GetType()}");

            // todo: handle filters

            var typeForConverter = inspector.GetObservableType(destinationInput.GetType());

            if (!destination.SourceType.IsAssignableFrom(typeForConverter))
            {
                var converter = ConverterRepository.Get(typeForConverter, destination.SourceType);
                destinationInput = converter.Convert(destinationInput);
            }

            var destinationTask = destination.Process(destinationInput);
            return destinationTask;
        }
    }
}