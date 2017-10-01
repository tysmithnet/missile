using System;
using System.Collections.Generic;
using FluentAssertions;
using Xunit;

namespace Missile.TextLauncher.Tests
{
    public class ConverterRepository_Should
    {
        [Fact]
        public void Return_Compatible_Match_If_One_Exists()
        {
            var converterRepository = new ConverterRepositoryBuilder()
                .WithConverter(new DogSnake())
                .Build();

            var converter = converterRepository.Get(typeof(Corgi), typeof(Snake));
            converter.ConverterInstance.GetType().Should().Be(typeof(DogSnake),
                "compatible matches should return if the actual type isn't there");
        }

        [Fact]
        public void Return_Exact_Match_If_One_Exists()
        {
            var converterRepository = new ConverterRepositoryBuilder()
                .WithConverter(new CorgiSnake())
                .Build();

            var converter = converterRepository.Get(typeof(Corgi), typeof(Snake));
            converter.ConverterInstance.GetType().Should().Be(typeof(CorgiSnake), "exact matches should always return");
        }

        [Fact]
        public void Throw_If_No_Match()
        {
            var converterRepository = new ConverterRepositoryBuilder()
                .WithConverter(new CorgiPython())
                .Build();

            converterRepository.Invoking(r => r.Get(typeof(string), typeof(int)))
                .ShouldThrow<KeyNotFoundException>("if no compatible converter exists, then throw an exception");
        }

        [Fact]
        public void Work_For_Interfaces()
        {
            var converterRepository = new ConverterRepositoryBuilder()
                .WithConverter(new LegsToNoLegs())
                .Build();

            var converter = converterRepository.Get(typeof(IQuadraped), typeof(INoLegs));
            converter.ConverterInstance.GetType().Should().Be(typeof(LegsToNoLegs),
                "compatible matches should return if the actual type isn't there");
        }
    }
}