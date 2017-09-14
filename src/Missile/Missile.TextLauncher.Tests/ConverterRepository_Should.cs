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
        private interface IHasLegs
        {

        }

        private interface IQuadraped : IHasLegs
        {

        }

        private interface IBiPed : IHasLegs
        {

        }

        private interface INoLegs
        {

        }

        private class Mammal
        {
        }

        private class Amphibion
        {
        }

        private class Dog : Mammal, IQuadraped
        {
        }

        private class Primate : Mammal, IBiPed
        {
        }

        private class Corgi : Dog
        {
        }

        private class Chimp : Primate
        {
        }

        private class Turtle : Amphibion, IQuadraped
        {
        }

        private class Leatherback : Turtle
        {
        }

        private class Snake : Amphibion, INoLegs
        {
        }

        private class Python : Snake
        {
        }

        private class CorgiSnake : IConverter<Corgi, Snake>
        {
            public IObservable<Snake> Convert(IObservable<Corgi> source)
            {
                throw new NotImplementedException();
            }
        }

        private class CorgiPython : IConverter<Corgi, Python>
        {
            public IObservable<Python> Convert(IObservable<Corgi> source)
            {
                throw new NotImplementedException();
            }
        }

        private class DogSnake : IConverter<Dog, Snake>
        {
            public IObservable<Snake> Convert(IObservable<Dog> source)
            {
                throw new NotImplementedException();
            }
        }

        private class LegsToNoLegs : IConverter<IHasLegs, INoLegs>
        {
            public IObservable<INoLegs> Convert(IObservable<IHasLegs> source)
            {
                throw new NotImplementedException();
            }
        }

        [Fact]
        public void Return_Exact_Match_If_One_Exists()
        {
            ConverterRepository converterRepository = new ConverterRepositoryBuilder()
                .WithConverter(new CorgiSnake())
                .Build();

            var converter = converterRepository.Get(typeof(Corgi), typeof(Snake));
            converter.ConverterInstance.GetType().Should().Be(typeof(CorgiSnake), "exact matches should always return");
        }

        [Fact]
        public void Return_Compatible_Match_If_One_Exists()
        {
            ConverterRepository converterRepository = new ConverterRepositoryBuilder()
                .WithConverter(new DogSnake())
                .Build();

            var converter = converterRepository.Get(typeof(Corgi), typeof(Snake));
            converter.ConverterInstance.GetType().Should().Be(typeof(DogSnake), "compatible matches should return if the actual type isn't there");
        }

        [Fact]
        public void Return_Closest_Match()
        {
            //ConverterRepository converterRepository = new ConverterRepositoryBuilder()
            //    .WithConverter(new )
            //    .Build();

            //var converter = converterRepository.Get(typeof(string), typeof(string));
            //converter.ConverterInstance.GetType().Should().Be(typeof(ObjectStringConverter), "compatible matches should return if the actual type isn't there");
        }

        [Fact]
        public void Throw_If_No_Match()
        {
            ConverterRepository converterRepository = new ConverterRepositoryBuilder()
                .WithConverter(new CorgiPython())
                .Build();

            converterRepository.Invoking(r => r.Get(typeof(string), typeof(int)))
                .ShouldThrow<IndexOutOfRangeException>("if no compatible converter exists, then throw an exception");
        }
    }
}
