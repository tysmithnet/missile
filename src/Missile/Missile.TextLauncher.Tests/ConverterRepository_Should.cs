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
            converter.ConverterInstance.GetType().Should().Be(typeof(ObjectStringConverter), "if an exact match exists");
        }
    }
}
