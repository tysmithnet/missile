using System;
using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using Xunit;

namespace Missile.TextLauncher.Tests
{
    [ExcludeFromCodeCoverage]
    public class PropertyFieldAdapter_Should
    {
        private class Person
        {
            public string Name { get; set; }
            public int Age { get; set; }

            public void Print()
            {
            }
        }

        [Fact]
        public void Throw_If_Arguments_Are_Invalid()
        {
            Action a = () => new PropertyFieldAdapter(typeof(Person).GetProperty("Name"), null);
            a.ShouldThrow<ArgumentNullException>();
            Action a2 = () => new PropertyFieldAdapter(typeof(Person).GetMethod("Print"), new Person());
            a2.ShouldThrow<ArgumentException>();
        }
    }
}