using System;
using System.Collections.Generic;
using FluentAssertions;
using Missile.TextLauncher.Interpretation.Tests.Mocks;
using Xunit;

namespace Missile.TextLauncher.Interpretation.Tests
{
    public class Interpreter_Should
    {
        [Fact]
        public void Handle_Most_Basic_Use_Case()
        {
            var rootNodeBuilder = new RootNodeBuilder();
            rootNodeBuilder.WithProvider("noop");
            var rootNode = rootNodeBuilder.Build();
            var interpreter = new Interpreter();
            interpreter.ProviderRepository = new ProviderRepository();
            interpreter.ProviderRepository.RegisteredProviders = new List<RegisteredProvider>
            {
                new RegisteredProvider
                {
                    Name = "noop",
                    DestinationType = typeof(object),
                    ProviderInstance = new NoopProvider(),
                    ProvideMethodInfo = typeof(NoopProvider).GetMethod("Provide")
                }
            };
            var task = interpreter.Interpret(rootNode);
            Action action = () => task.Wait();
            action.ShouldNotThrow("most basic case should always pass");
        }

        [Fact]
        public void Handle_No_Filter_Case()
        {
            var rootNodeBuilder = new RootNodeBuilder();
            rootNodeBuilder
                .WithProvider("string")
                .WithDestination("console");
            var rootNode = rootNodeBuilder.Build();
            var interpreter = new Interpreter();
            interpreter.ProviderRepository = new ProviderRepository();
            interpreter.ProviderRepository.RegisteredProviders = new List<RegisteredProvider>
            {
                new RegisteredProvider
                {
                    Name = "noop",
                    DestinationType = typeof(object),
                    ProviderInstance = new NoopProvider(),
                    ProvideMethodInfo = typeof(NoopProvider).GetMethod("Provide")
                }
            };
            var task = interpreter.Interpret(rootNode);
            Action action = () => task.Wait();
            action.ShouldNotThrow("most basic case should always pass");
        }
    }
}