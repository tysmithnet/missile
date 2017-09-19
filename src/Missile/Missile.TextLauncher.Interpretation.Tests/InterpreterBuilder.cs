using Missile.TextLauncher.Destination;
using Missile.TextLauncher.Filtration;
using Missile.TextLauncher.Provision;

namespace Missile.TextLauncher.Interpretation.Tests
{
    public class InterpreterBuilder
    {
        public InterpreterBuilder()
        {
            Interpreter = new Interpreter
            {
                ProviderRepository = new ProviderRepository(),
                FilterRepository = new FilterRepository(),
                DestinationRepository = new DestinationRepository()
            };
        }

        protected Interpreter Interpreter { get; set; }

        public InterpreterBuilder WithProvider(RegisteredProvider provider)
        {
            Interpreter.ProviderRepository.Add(provider);
            return this;
        }

        public InterpreterBuilder WithFilter(RegisteredFilter filter)
        {
            Interpreter.FilterRepository.RegisteredFilters.Add(filter);
            return this;
        }

        public InterpreterBuilder WithDestination(RegisteredDestination destination)
        {
            Interpreter.DestinationRepository.Add(destination);
            return this;
        }

        public Interpreter Build()
        {
            return Interpreter;
        }
    }
}