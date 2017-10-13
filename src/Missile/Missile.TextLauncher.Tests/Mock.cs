using System;
using System.Diagnostics.CodeAnalysis;
using Missile.TextLauncher.Conversion;

namespace Missile.TextLauncher.Tests
{
    public interface IHasLegs
    {
    }

    public interface IQuadraped : IHasLegs
    {
    }

    public interface IBiPed : IHasLegs
    {
    }

    public interface INoLegs
    {
    }

    [ExcludeFromCodeCoverage]
    public class Mammal
    {
    }

    [ExcludeFromCodeCoverage]
    public class Amphibion
    {
    }

    [ExcludeFromCodeCoverage]
    public class Dog : Mammal, IQuadraped
    {
    }

    [ExcludeFromCodeCoverage]
    public class Primate : Mammal, IBiPed
    {
    }

    [ExcludeFromCodeCoverage]
    public class Corgi : Dog
    {
    }

    [ExcludeFromCodeCoverage]
    public class Chimp : Primate
    {
    }

    [ExcludeFromCodeCoverage]
    public class Turtle : Amphibion, IQuadraped
    {
    }

    [ExcludeFromCodeCoverage]
    public class Leatherback : Turtle
    {
    }

    [ExcludeFromCodeCoverage]
    public class Snake : Amphibion, INoLegs
    {
    }

    [ExcludeFromCodeCoverage]
    public class Python : Snake
    {
    }

    [ExcludeFromCodeCoverage]
    public class CorgiSnake : IConverter<Corgi, Snake>
    {
        public IObservable<Snake> Convert(IObservable<Corgi> source)
        {
            throw new NotImplementedException();
        }
    }

    [ExcludeFromCodeCoverage]
    public class CorgiPython : IConverter<Corgi, Python>
    {
        public IObservable<Python> Convert(IObservable<Corgi> source)
        {
            throw new NotImplementedException();
        }
    }

    [ExcludeFromCodeCoverage]
    public class DogSnake : IConverter<Dog, Snake>
    {
        public IObservable<Snake> Convert(IObservable<Dog> source)
        {
            throw new NotImplementedException();
        }
    }

    [ExcludeFromCodeCoverage]
    public class LegsToNoLegs : IConverter<IHasLegs, INoLegs>
    {
        public IObservable<INoLegs> Convert(IObservable<IHasLegs> source)
        {
            throw new NotImplementedException();
        }
    }
}