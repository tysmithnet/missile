using System;
using System.Text;
using FluentAssertions;
using Missile.TextLauncher.Destinations;
using Missile.TextLauncher.Filters;
using Missile.TextLauncher.Interpretation.Parsing;
using Missile.TextLauncher.Providers;
using Xunit;

namespace Missile.TextLauncher.Interpretation.Tests
{
    public class Interpreter_Should
    {
        [Fact]
        public void Handle_Most_Basic_Use_Case()
        {
            var rootNodeBuilder = new RootNodeBuilder();
            rootNodeBuilder.WithProvider("noop", new string[0]);
            var rootNode = rootNodeBuilder.Build();
            var interpreterBuilder = new InterpreterBuilder()
                .WithProvider(new RegisteredProvider
                {
                    Name = "noop",
                    DestinationType = typeof(object),
                    ProviderInstance = new NoOpProvider(),
                    ProvideMethodInfo = typeof(NoOpProvider).GetMethod("Provide")
                })
                .WithDestination(new RegisteredDestination
                {
                    Name = "noop",
                    SourceType = typeof(object),
                    DestinationInstance = new NoOpDestination(),
                    ProcessAsyncMethodInfo = typeof(NoOpDestination).GetMethod("ProcessAsync")
                });

            var interpreter = interpreterBuilder.Build();
            var task = interpreter.Interpret(rootNode);
            Action action = () => task.Wait();
            action.ShouldNotThrow("single provider should be no problem");
        }

        [Fact]
        public void Handle_Multiple_Filters()
        {
            var rootNodeBuilder = new RootNodeBuilder();
            rootNodeBuilder
                .WithProvider("range", new string[0])
                .WithFilter("take", new string[0])
                .WithFilter("distinct", new string[0])
                .WithDestination("console", new string[0]);
            var sb = new StringBuilder();
            var interpreterBuilder = new InterpreterBuilder()
                .WithProvider(new RegisteredProvider
                {
                    Name = "range",
                    DestinationType = typeof(int),
                    ProviderInstance = new RangeProvider(),
                    ProvideMethodInfo = typeof(RangeProvider).GetMethod("Provide")
                }).WithFilter(new RegisteredFilter
                {
                    Name = "take",
                    FilterInstance = new TakeFilter(),
                    FilterMethodInfo = typeof(TakeFilter).GetMethod("Filter")
                }).WithFilter(new RegisteredFilter
                {
                    Name = "distinct",
                    FilterInstance = new DistinctFilter(),
                    FilterMethodInfo = typeof(DistinctFilter).GetMethod("Filter")
                }).WithDestination(new RegisteredDestination
                {
                    Name = "console",
                    SourceType = typeof(object),
                    DestinationInstance = new ConsoleDestination
                    {
                        WriteFunction = o => sb.AppendLine($"{o}")
                    },
                    ProcessAsyncMethodInfo = typeof(ConsoleDestination).GetMethod("ProcessAsync")
                });

            var task = interpreterBuilder.Build().Interpret(rootNodeBuilder.Build());
            Action action = async () => await task;
            action.ShouldNotThrow("interpreter should handle multiple filters");
            sb.Length.Should().BeGreaterThan(0);
        }

        [Fact]
        public void Handle_Provider_Filter_Destination()
        {
            var rootNodeBuilder = new RootNodeBuilder();
            rootNodeBuilder
                .WithProvider("range", new string[0])
                .WithFilter("take", new string[0])
                .WithDestination("console", new string[0]);

            var sb = new StringBuilder();
            var interpreterBuilder = new InterpreterBuilder()
                .WithProvider(new RegisteredProvider
                {
                    Name = "range",
                    DestinationType = typeof(int),
                    ProviderInstance = new RangeProvider(),
                    ProvideMethodInfo = typeof(RangeProvider).GetMethod("Provide")
                }).WithFilter(new RegisteredFilter
                {
                    Name = "take",
                    FilterInstance = new TakeFilter(),
                    FilterMethodInfo = typeof(TakeFilter).GetMethod("Filter")
                }).WithFilter(new RegisteredFilter
                {
                    Name = "distinct",
                    FilterInstance = new DistinctFilter(),
                    FilterMethodInfo = typeof(DistinctFilter).GetMethod("Filter")
                }).WithDestination(new RegisteredDestination
                {
                    Name = "console",
                    SourceType = typeof(object),
                    DestinationInstance = new ConsoleDestination
                    {
                        WriteFunction = o => sb.AppendLine($"{o}")
                    },
                    ProcessAsyncMethodInfo = typeof(ConsoleDestination).GetMethod("ProcessAsync")
                });

            var task = interpreterBuilder.Build().Interpret(rootNodeBuilder.Build());
            Action action = async () => await task;
            action.ShouldNotThrow("provider, filter, destination combo should work");
            sb.Length.Should().BeGreaterThan(0);
        }

        [Fact]
        public void Handle_Provider_To_Destination()
        {
            var rootNodeBuilder = new RootNodeBuilder();
            rootNodeBuilder
                .WithProvider("range", new string[0])
                .WithDestination("console", new string[0]);

            var sb = new StringBuilder();
            var interpreterBuilder = new InterpreterBuilder()
                .WithProvider(new RegisteredProvider
                {
                    Name = "range",
                    DestinationType = typeof(int),
                    ProviderInstance = new RangeProvider(),
                    ProvideMethodInfo = typeof(RangeProvider).GetMethod("Provide")
                }).WithDestination(new RegisteredDestination
                {
                    Name = "console",
                    SourceType = typeof(object),
                    DestinationInstance = new ConsoleDestination
                    {
                        WriteFunction = o => sb.AppendLine($"{o}")
                    },
                    ProcessAsyncMethodInfo = typeof(ConsoleDestination).GetMethod("ProcessAsync")
                });


            var task = interpreterBuilder.Build().Interpret(rootNodeBuilder.Build());
            Action action = async () => await task;
            action.ShouldNotThrow("provider and destination combo should work");
            sb.Length.Should().BeGreaterThan(0);
        }
    }
}