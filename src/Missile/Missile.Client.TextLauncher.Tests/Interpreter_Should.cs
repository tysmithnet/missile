using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
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


    public class Interpreter_Should
    {
        [Fact]
        public void Load_The_Appropriate_Provider()
        {
            var providerRepoMock = new Mock<IProviderRepository>();
            providerRepoMock.Setup(repository => repository.Get("string")).Returns(() => new StringProvider());

            var filterRepoMock = new Mock<IFilterRepository>();
            filterRepoMock.Setup(repository => repository.Get("distinct")).Returns(() => new DistinctFilter());

            Interpreter interpreter = new Interpreter();
            interpreter.ProviderRepository = providerRepoMock.Object;
            interpreter.FilterRepository = filterRepoMock.Object;
            RootNode rootNode = new RootNode();
            rootNode.ProviderNode = new ProviderNode(new ProviderToken("string"));
            rootNode.FilterNodes = new List<FilterNode>()
            {
                new FilterNode(new FilterToken("distinct"))
            };
            interpreter.Interpret(rootNode);
        }
    }
}
