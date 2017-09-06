using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using FluentAssertions;
using Missile.TextLauncher.Interpretation.Compilation;
using Moq;
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
            action.ShouldNotThrow("single provider should be no problem");
        }

        [Fact]
        public void Handle_Provider_To_Destination()
        {
            var rootNodeBuilder = new RootNodeBuilder();
            rootNodeBuilder
                .WithProvider("range")
                .WithDestination("console");        
            var interpreter = new Interpreter();
            interpreter.ProviderRepository = new ProviderRepository();
            interpreter.ProviderRepository.RegisteredProviders = new List<RegisteredProvider>
            {
                new RegisteredProvider
                {
                    Name = "range",
                    DestinationType = typeof(int),
                    ProviderInstance = new RangeProvider(),
                    ProvideMethodInfo = typeof(RangeProvider).GetMethod("Provide")
                }
            };

            interpreter.DestinationRepository = new DestinationRepository();
            StringBuilder sb = new StringBuilder();
            interpreter.DestinationRepository.RegisteredDestinations = new List<RegisteredDestination>()
            {
                new RegisteredDestination
                {
                    Name = "console",
                    SourceType = typeof(object),
                    DestinationInstance = new ConsoleDestination
                    {
                        WriteFunction = o => sb.AppendLine($"{o}")
                    },
                    ProcessAsyncMethodInfo = typeof(ConsoleDestination).GetMethod("ProcessAsync")
                }
            };
            
            var task = interpreter.Interpret(rootNodeBuilder.Build());
            Action action = async () => await task;
            action.ShouldNotThrow("provider and destination combo should work");
            sb.Length.Should().BeGreaterThan(0);
        }

        [Fact]
        public void Handle_Provider_Filter_Destination()
        {
            var rootNodeBuilder = new RootNodeBuilder();
            rootNodeBuilder
                .WithProvider("range")
                .WithFilter("take")
                .WithDestination("console");
            var interpreter = new Interpreter();
            interpreter.ProviderRepository = new ProviderRepository();
            interpreter.ProviderRepository.RegisteredProviders = new List<RegisteredProvider>
            {
                new RegisteredProvider
                {
                    Name = "range",
                    DestinationType = typeof(int),
                    ProviderInstance = new RangeProvider(),
                    ProvideMethodInfo = typeof(RangeProvider).GetMethod("Provide")
                }
            };

            interpreter.FilterRepository = new FilterRepository();
            interpreter.FilterRepository.RegisteredFilters = new List<RegisteredFilter>()
            {
                new RegisteredFilter
                {
                    Name = "take",
                    FilterInstance = new TakeFilter(),
                    FilterMethodInfo = typeof(TakeFilter).GetMethod("Filter")
                }
            };

            interpreter.DestinationRepository = new DestinationRepository();
            StringBuilder sb = new StringBuilder();
            interpreter.DestinationRepository.RegisteredDestinations = new List<RegisteredDestination>()
            {
                new RegisteredDestination
                {
                    Name = "console",
                    SourceType = typeof(object),
                    DestinationInstance = new ConsoleDestination
                    {
                        WriteFunction = o => sb.AppendLine($"{o}")
                    },
                    ProcessAsyncMethodInfo = typeof(ConsoleDestination).GetMethod("ProcessAsync")
                }
            };

            var task = interpreter.Interpret(rootNodeBuilder.Build());
            Action action = async () => await task;
            action.ShouldNotThrow("provider, filter, destination combo should work");
            sb.Length.Should().BeGreaterThan(0);
        }

        [Fact]
        public void Handle_Multiple_Filters()
        {
            var rootNodeBuilder = new RootNodeBuilder();
            rootNodeBuilder
                .WithProvider("range")
                .WithFilter("take")
                .WithFilter("distinct")
                .WithDestination("console");
            var interpreter = new Interpreter();
            interpreter.ProviderRepository = new ProviderRepository();
            interpreter.ProviderRepository.RegisteredProviders = new List<RegisteredProvider>
            {
                new RegisteredProvider
                {
                    Name = "range",
                    DestinationType = typeof(int),
                    ProviderInstance = new RangeProvider(),
                    ProvideMethodInfo = typeof(RangeProvider).GetMethod("Provide")
                }
            };

            interpreter.FilterRepository = new FilterRepository();
            interpreter.FilterRepository.RegisteredFilters = new List<RegisteredFilter>()
            {
                new RegisteredFilter
                {
                    Name = "take",
                    FilterInstance = new TakeFilter(),
                    FilterMethodInfo = typeof(TakeFilter).GetMethod("Filter")
                },
                new RegisteredFilter
                {
                    Name = "distinct",
                    FilterInstance = new DistinctFilter(),
                    FilterMethodInfo = typeof(DistinctFilter).GetMethod("Filter")
                }
            };

            interpreter.DestinationRepository = new DestinationRepository();
            StringBuilder sb = new StringBuilder();
            interpreter.DestinationRepository.RegisteredDestinations = new List<RegisteredDestination>()
            {
                new RegisteredDestination
                {
                    Name = "console",
                    SourceType = typeof(object),
                    DestinationInstance = new ConsoleDestination
                    {
                        WriteFunction = o => sb.AppendLine($"{o}")
                    },
                    ProcessAsyncMethodInfo = typeof(ConsoleDestination).GetMethod("ProcessAsync")
                }
            };

            var task = interpreter.Interpret(rootNodeBuilder.Build());
            Action action = async () => await task;
            action.ShouldNotThrow("interpreter should handle multiple filters");
            sb.Length.Should().BeGreaterThan(0);
        }
    }
}