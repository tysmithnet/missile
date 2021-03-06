﻿using System;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using System.Threading;
using FluentAssertions;
using Missile.TextLauncher.Conversion;
using Missile.TextLauncher.Destination;
using Missile.TextLauncher.Filtration;
using Missile.TextLauncher.Interpretation.Parsing;
using Missile.TextLauncher.Interpretation.Tests.Mocks;
using Missile.TextLauncher.Provision;
using Missile.TextLauncher.Provision.Range;
using Moq;
using Xunit;

namespace Missile.TextLauncher.Interpretation.Tests
{
    [ExcludeFromCodeCoverage]
    public class Interpreter_Should
    {
        private class UnknownObservable<T, U> : IObservable<T>
        {
            public U Something { get; set; }

            public IDisposable Subscribe(IObserver<T> observer)
            {
                throw new NotImplementedException();
            }
        }

        [Fact]
        public void Handle_Conversion()
        {
            var rootNodeBuilder = new RootNodeBuilder();
            rootNodeBuilder
                .WithProvider("mockobject", new string[0])
                .WithDestination("mockstring", new string[0]);

            var interpreterBuilder = new InterpreterBuilder()
                .WithProvider(new RegisteredProvider
                {
                    Name = "mockobject",
                    ProviderInstance = new MockObjectProvider(),
                    ProvideMethodInfo = typeof(MockObjectProvider).GetMethod("Provide")
                }).WithConverter(new RegisteredConverter
                {
                    SourceType = typeof(object),
                    DestType = typeof(string),
                    ConverterInstance = new MockObjectStringConverter(),
                    ConvertMethodInfo = typeof(MockObjectStringConverter).GetMethod("Convert")
                })
                .WithDestination(new RegisteredDestination
                {
                    Name = "mockstring",
                    SourceType = typeof(string),
                    DestinationInstance = new MockStringDestination(),
                    ProcessAsyncMethodInfo = typeof(MockStringDestination).GetMethod("ProcessAsync")
                });


            var task = interpreterBuilder.Build().InterpretAsync(rootNodeBuilder.Build(), CancellationToken.None);
            Action action = async () => await task;
            action.ShouldNotThrow(
                "conversion should be used if the output type of a provider is not assignable to the source type of the destination");
        }

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
            var task = interpreter.InterpretAsync(rootNode, CancellationToken.None);
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
                    ProviderInstance = new RangeProvider(),
                    ProvideMethodInfo = typeof(RangeProvider).GetMethod("Provide")
                }).WithFilter(new RegisteredFilter
                {
                    Name = "take",
                    FilterInstance = new HeadFilter(),
                    FilterMethodInfo = typeof(HeadFilter).GetMethod("Filter")
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

            var task = interpreterBuilder.Build().InterpretAsync(rootNodeBuilder.Build(), CancellationToken.None);
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
                    ProviderInstance = new RangeProvider(),
                    ProvideMethodInfo = typeof(RangeProvider).GetMethod("Provide")
                }).WithFilter(new RegisteredFilter
                {
                    Name = "take",
                    FilterInstance = new HeadFilter(),
                    FilterMethodInfo = typeof(HeadFilter).GetMethod("Filter")
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

            var task = interpreterBuilder.Build().InterpretAsync(rootNodeBuilder.Build(), CancellationToken.None);
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

            var interpreter = interpreterBuilder.Build();
            var rootNode = rootNodeBuilder.Build();
            interpreter.Invoking(async i => await i.InterpretAsync(rootNode, CancellationToken.None))
                .ShouldNotThrow("provider and destination combo should work");
            sb.Length.Should().BeGreaterThan(0);
        }

        [Fact]
        public void Throw_ApplicationException_If_There_Is_No_Suitable_Inspector()
        {
            var rootNodeBuilder = new RootNodeBuilder();
            rootNodeBuilder
                .WithProvider("range", new string[0])
                .WithDestination("console", new string[0]);

            var sb = new StringBuilder();
            var mockRegisteredProvider = new Mock<RegisteredProvider>();
            mockRegisteredProvider.Object.Name = "range";
            mockRegisteredProvider.Setup(provider => provider.Provide(It.IsAny<string[]>()))
                .Returns(new UnknownObservable<object, int>());

            var interpreterBuilder = new InterpreterBuilder()
                .WithProvider(mockRegisteredProvider.Object).WithDestination(new RegisteredDestination
                {
                    Name = "console",
                    SourceType = typeof(object),
                    DestinationInstance = new ConsoleDestination
                    {
                        WriteFunction = o => sb.AppendLine($"{o}")
                    },
                    ProcessAsyncMethodInfo = typeof(ConsoleDestination).GetMethod("ProcessAsync")
                });

            var interpreter = interpreterBuilder.Build();
            var rootNode = rootNodeBuilder.Build();
            interpreter.Invoking(i => i.InterpretAsync(rootNode, CancellationToken.None))
                .ShouldThrow<ApplicationException>();
        }
    }
}