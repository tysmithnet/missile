using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Missile.TextLauncher.Destination;
using Xunit;

namespace Missile.TextLauncher.Tests
{
    public class ConsoleDestination_Should
    {
        [Fact]
        public async Task Write_Using_The_Provided_Action()
        {
            bool isWrittenTo = false;
            Action<object> action = (o) => isWrittenTo = true;
            var dest = new ConsoleDestination()
            {
                WriteFunction = action
            };

            await dest.ProcessAsync(Observable.Range(0, 1).Select(x => x as object), CancellationToken.None);
        }
    }
}
