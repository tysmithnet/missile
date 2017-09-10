using System.ComponentModel.Composition;
using Microsoft.Extensions.Logging;
using ILogger = Missile.Core.ILogger;

namespace Missile.Client
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