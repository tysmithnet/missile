using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using Missile.Client.TextLauncher.Compilation;

namespace Missile.Client.TextLauncher.Temp
{
    public class GoogleProvider : IProvider<GoogleResult>
    {
        public IObservable<GoogleResult> Get(string[] args)
        {
            var results = Enumerable.Range(1, 3).Select(x => new GoogleResult
            {
                Title = $"{x}",
                Url =  $"http://wwww.{x}.com"
            });
            return results.ToObservable();
        }

        public string Name { get; }
        IObservable<GoogleResult> IProvider<GoogleResult>.Get(string argString)
        {
            throw new NotImplementedException();
        }
    }

    public class GoogleResult
    {
        public string Title { get; set; }
        public string Url { get; set; }
    }

    public class GoogleStringConverter : IConverter<GoogleResult, string>
    {                                           
        public IObservable<string> Convert(IObservable<GoogleResult> source)
        {
            throw new NotImplementedException();
        }

    }
}
