using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Reactive.Linq;
using System.Threading;
using FluentAssertions;
using Missile.TextLauncher.Destination;
using Xunit;

namespace Missile.TextLauncher.Tests
{
    [ExcludeFromCodeCoverage]
    public class ConsoleDestination_Should
    {
        [Fact]
        public void Throw_An_Exception_If_Error()
        {
            IEnumerable<object> Gen()
            {
                yield return 1;
                throw new FormatException();
            }

            var dest = new ConsoleDestination
            {
                WriteFunction = o => { }
            };
            dest.Invoking(destination =>
                destination.ProcessAsync(Gen().ToObservable(),
                    CancellationToken.None).Wait()).ShouldThrow<FormatException>();
        }

        [Fact]
        public void Write_Using_The_Provided_Action()
        {
            var isWrittenTo = false;

            void Action(object o)
            {
                isWrittenTo = true;
            }

            var dest = new ConsoleDestination
            {
                WriteFunction = Action
            };

            dest.ProcessAsync(Observable.Range(0, 1).Select(x => x as object), CancellationToken.None).Wait();
            isWrittenTo.Should().BeTrue();
        }
    }
}