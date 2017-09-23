using System.ComponentModel.Composition;
using Microsoft.Extensions.Logging;

namespace Missile.Core
{
    [Export(typeof(ILogger))]
    public class DefaultLogger : ILogger
    {
        public DefaultLogger()
        {
            var loggerFactory = new LoggerFactory()
#if DEBUG
                .AddDebug();
#else
                .AddConsole();
#endif
            Logger = loggerFactory.CreateLogger<DefaultLogger>();
        }

        protected internal Microsoft.Extensions.Logging.ILogger Logger { get; set; }

        public void Information(string message)
        {
            Logger.LogInformation(message);
        }
    }
}