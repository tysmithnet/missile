using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }

    public class GoogleResult
    {
        public string Title { get; set; }
        public string Url { get; set; }
    }

    public class GoogleStringConverter : IFilter<GoogleResult, string>
    {
        public IObservable<string> Filter(IObservable<GoogleResult> source)
        {
            return source.Select(x => $"{x.Title}: {x.Url}");
        }
    }
}
