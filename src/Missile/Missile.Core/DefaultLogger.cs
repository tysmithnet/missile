using System.ComponentModel.Composition;
using Microsoft.Extensions.Logging;
using MsILogger = Microsoft.Extensions.Logging.ILogger;

namespace Missile.Core
{
    /// <summary>
    ///     Default ILogger implementation provided out of the box
    /// </summary>
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

        protected internal MsILogger Logger { get; set; }

        public void Information(string message)
        {
            Logger.LogInformation(message);
        }
    }
}