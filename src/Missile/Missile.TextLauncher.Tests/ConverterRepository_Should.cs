using System;
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
        public void Choose_The_Closer_Match()
        {
            var converterRepository = new ConverterRepositoryBuilder()
                .WithConverter(new CorgiPython())
                .WithConverter(new CorgiSnake())
                .Build();

            var converter = converterRepository.Get(typeof(Corgi), typeof(Python));
            converter.GetType().Should().Be(typeof(CorgiPython),
                "converter repository should return the closer match");
        }

        [Fact]
        public void Return_Compatible_Match_If_One_Exists()
        {
            var converterRepository = new ConverterRepositoryBuilder()
                .WithConverter(new DogSnake())
                .Build();

            var converter = converterRepository.Get(typeof(Corgi), typeof(Amphibion));
            converter.ConverterInstance.GetType().Should().Be(typeof(DogSnake),
                "compatible matches should return if the actual type isn't there");
        }


        [Fact]
        public void Return_Exact_Match_If_One_Exists()
        {
            var converterRepository = new ConverterRepositoryBuilder()
                .WithConverter(new CorgiPython())
                .Build();

            var converter = converterRepository.Get(typeof(Corgi), typeof(Python));
            converter.ConverterInstance.GetType().Should()
                .Be(typeof(CorgiPython), "exact matches should always return");
        }

        [Fact]
        public void Throw_If_No_Match()
        {
            var converterRepository = new ConverterRepositoryBuilder()
                .WithConverter(new CorgiSnake())
                .Build();

            converterRepository.Invoking(r => r.Get(typeof(Mammal), typeof(Turtle)))
                .ShouldThrow<IndexOutOfRangeException>("if no compatible converter exists, then throw an exception");
        }

        [Fact]
        public void Return_Exact_Match_For_Interfaces()
        {
            var converterRepository = new ConverterRepositoryBuilder()
                .WithConverter(new LegsToNoLegs())
                .Build();

            var converter = converterRepository.Get(typeof(IHasLegs), typeof(INoLegs));
            converter.ConverterInstance.GetType().Should()
                .Be(typeof(LegsToNoLegs), "exact matches should always return");
        }

        [Fact]
        public void Return_Compatible_Match_For_Interfaces()
        {
            var converterRepository = new ConverterRepositoryBuilder()
                .WithConverter(new LegsToNoLegs())
                .Build();

            var converter = converterRepository.Get(typeof(IQuadraped), typeof(INoLegs));
            converter.ConverterInstance.GetType().Should()
                .Be(typeof(LegsToNoLegs), "exact matches should always return");
        }
    }
}