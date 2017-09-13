using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;

namespace Missile.TextLauncher.Tests
{
    public class ConverterRepository_Should
    {
        public class ObjectStringConverter : IConverter<object, string>
        {
            public IObservable<string> Convert(IObservable<object> source)
            {
                return source.Select(o => o.ToString());
            }
        }
        

        [Fact]
        public void Return_Exact_Match_If_One_Exists()
        {
            ConverterRepository converterRepository = new ConverterRepositoryBuilder()
                .WithConverter(new ObjectStringConverter())
                .Build();

            var converter = converterRepository.Get(typeof(object), typeof(string));
            converter.ConverterInstance.GetType().Should().Be(typeof(ObjectStringConverter), "exact matches should always return");
        }

        [Fact]
        public void Return_Compatible_Match_If_One_Exists()
        {
            ConverterRepository converterRepository = new ConverterRepositoryBuilder()
                .WithConverter(new ObjectStringConverter())
                .Build();

            var converter = converterRepository.Get(typeof(string), typeof(string));
            converter.ConverterInstance.GetType().Should().Be(typeof(ObjectStringConverter), "compatible matches should return if the actual type isn't there");
        }

        [Fact]
        public void Throw_If_No_Match()
        {
            ConverterRepository converterRepository = new ConverterRepositoryBuilder()
                .WithConverter(new ObjectStringConverter())
                .Build();

            converterRepository.Invoking(r => r.Get(typeof(string), typeof(int)))
                .ShouldThrow<IndexOutOfRangeException>("if no compatible converter exists, then throw an exception");
        }
    }
}
