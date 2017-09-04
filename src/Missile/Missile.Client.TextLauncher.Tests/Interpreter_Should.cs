﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Missile.Client.TextLauncher.Compilation;
using Moq;
using Xunit;

namespace Missile.Client.TextLauncher.Tests
{
    public class StringProvider : IProvider<string>
    {
        public IObservable<string> Get(string argString)
        {
            return Enumerable.Range(0, 10).Select(x => $"{x}").ToObservable();
        }

        public string Name { get; } = "string";
    }

    public class DistinctFilter : IFilter<string, string>
    {
        public IObservable<string> Filter(IObservable<string> source)
        {
            return source.Distinct();
        }

        public string Name { get; } = "distinct";
    }

    public class ConsoleDestination : IDestination<string>
    {
        public Task Process(IObservable<string> source)
        {
            TaskCompletionSource<object> tcs = new TaskCompletionSource<object>();
            source.Subscribe(Console.WriteLine, () => tcs.SetResult(null));
            return tcs.Task;
        }

        public string Name { get; } = "console";
    }

    public class Interpreter_Should
    {
        [Fact]
        public async Task Handle_Pipeline_Where_Conversion_Is_Not_Necessary()
        {
            var providerRepoMock = new Mock<IProviderRepository>();
            providerRepoMock.Setup(repository => repository.Get("string")).Returns(() => new StringProvider());

            var filterRepoMock = new Mock<IFilterRepository>();
            filterRepoMock.Setup(repository => repository.Get("distinct")).Returns(() => new DistinctFilter());

            var destinationRepoMock = new Mock<IDestinationRepository>();
            destinationRepoMock.Setup(repository => repository.Get("console")).Returns(() => new ConsoleDestination());

            Interpreter interpreter = new Interpreter();
            interpreter.ProviderRepository = providerRepoMock.Object;
            interpreter.FilterRepository = filterRepoMock.Object;
            interpreter.DestinationRepository = destinationRepoMock.Object;
            RootNode rootNode = new RootNode();
            rootNode.ProviderNode = new ProviderNode(new ProviderToken("string"));
            rootNode.FilterNodes = new List<FilterNode>()
            {
                new FilterNode(new FilterToken("distinct"))
            };
            rootNode.DestinationNode = new DestinationNode(new DestinationToken("console"));
            interpreter.Invoking(i => i.Interpret(rootNode)).ShouldNotThrow("the same type is passed between each pipeline component and thus requires no conversion");
        }
    }
}
