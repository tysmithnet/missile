using System;
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

    public class Mammal
    {
    }

    public class Amphibion
    {
    }

    public class Dog : Mammal, IQuadraped
    {
    }

    public class Primate : Mammal, IBiPed
    {
    }

    public class Corgi : Dog
    {
    }

    public class Chimp : Primate
    {
    }

    public class Turtle : Amphibion, IQuadraped
    {
    }

    public class Leatherback : Turtle
    {
    }

    public class Snake : Amphibion, INoLegs
    {
    }

    public class Python : Snake
    {
    }

    public class CorgiSnake : IConverter<Corgi, Snake>
    {
        public IObservable<Snake> Convert(IObservable<Corgi> source)
        {
            throw new NotImplementedException();
        }
    }

    public class CorgiPython : IConverter<Corgi, Python>
    {
        public IObservable<Python> Convert(IObservable<Corgi> source)
        {
            throw new NotImplementedException();
        }
    }

    public class DogSnake : IConverter<Dog, Snake>
    {
        public IObservable<Snake> Convert(IObservable<Dog> source)
        {
            throw new NotImplementedException();
        }
    }

    public class LegsToNoLegs : IConverter<IHasLegs, INoLegs>
    {
        public IObservable<INoLegs> Convert(IObservable<IHasLegs> source)
        {
            throw new NotImplementedException();
        }
    }
}