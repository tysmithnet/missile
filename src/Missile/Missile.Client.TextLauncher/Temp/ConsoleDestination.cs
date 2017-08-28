using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Missile.Client.TextLauncher.Temp
{
    public class ConsoleDestination : IDestination<string>
    {
        public void Process(IObservable<string> source)
        {
            source.Subscribe(Console.WriteLine, exception => Console.Error.WriteLine(exception),
                () => Console.WriteLine("Complete"));
        }
    }
}
