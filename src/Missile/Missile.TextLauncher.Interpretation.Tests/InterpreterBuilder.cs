﻿using System.Diagnostics.CodeAnalysis;
using Missile.TextLauncher.Conversion;
using Missile.TextLauncher.Destination;
using Missile.TextLauncher.Filtration;
using Missile.TextLauncher.Provision;

namespace Missile.TextLauncher.Interpretation.Tests
{
    [ExcludeFromCodeCoverage]
    public class InterpreterBuilder
    {
        public InterpreterBuilder()
        {
            Interpreter = new Interpreter
            {
                ProviderRepository = new ProviderRepository(),
                ConverterRepository = new ConverterRepository
                {
                    ConverterSelectionStrategy = new ConverterSelectionStrategy()
                },
                FilterRepository = new FilterRepository(),
                DestinationRepository = new DestinationRepository(),
                ObservableInspectors = new[]
                {
                    new SingleGenericTypeParameterInspector()
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
            Interpreter.FilterRepository.Add(filter);
            return this;
        }

        public InterpreterBuilder WithDestination(RegisteredDestination destination)
        {
            Interpreter.DestinationRepository.Register(destination);
            return this;
        }

        public Interpreter Build()
        {
            return Interpreter;
        }

        public InterpreterBuilder WithConverter(RegisteredConverter registeredConverter)
        {
            Interpreter.ConverterRepository.Register(registeredConverter);
            return this;
        }
    }
}