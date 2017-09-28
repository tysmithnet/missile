﻿using Missile.TextLauncher.Conversion;
using Missile.TextLauncher.Destination;
using Missile.TextLauncher.Filtration;
using Missile.TextLauncher.Interpretation.Parsing;
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
                ConverterRepository = new ConverterRepository(),
                FilterRepository = new FilterRepository(),
                DestinationRepository = new DestinationRepository(),
                ObservableInspectors = new[]
                {
                    new ToObservableInspector()
                }
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

        public InterpreterBuilder WithConverter(RegisteredConverter registeredConverter)
        {
            Interpreter.ConverterRepository.Add(registeredConverter);
            return this;
        }
    }
}