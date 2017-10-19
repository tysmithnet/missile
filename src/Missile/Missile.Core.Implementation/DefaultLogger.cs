using System.ComponentModel.Composition;
using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Logging;
using ILogger = Missile.Core.Logging.ILogger;
using MsILogger = Microsoft.Extensions.Logging.ILogger;

namespace Missile.Core.Implementation
{
    /// <inheritdoc />
    /// <summary>
    ///     Default ILogger implementation provided out of the box
    /// </summary>
    [Export(typeof(ILogger))]
    [ExcludeFromCodeCoverage]
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

        /// <summary>
        ///     Gets or sets the logger.
        /// </summary>
        /// <value>
        ///     The logger.
        /// </value>
        protected internal MsILogger Logger { get; set; }

        /// <summary>
        ///     Informations the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        public void Information(string message)
        {
            Logger.LogInformation(message);
        }
    }
}