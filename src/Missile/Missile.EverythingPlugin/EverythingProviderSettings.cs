using System;
using System.ComponentModel.Composition;
using System.Diagnostics.CodeAnalysis;
using Missile.Core;

namespace Missile.EverythingPlugin
{
    /// <inheritdoc />
    /// <summary>
    ///     Settings for EverythingProvider
    /// </summary>
    /// <seealso cref="T:Missile.TextLauncher.ISettings" />
    [Export(typeof(ISettings))]
    [Serializable]
    [ExcludeFromCodeCoverage]
    public class EverythingProviderSettings : ISettings
    {
        /// <summary>
        ///     Gets or sets the default number maximum results
        /// </summary>
        /// <value>
        ///     The default number maximum results
        /// </value>
        [Setting]
        public int DefaultNumMaxResults { get; set; } = 100;

        /// <summary>
        ///     Path to es.exe
        ///     https://www.voidtools.com/support/everything/command_line_interface/
        /// </summary>
        [Setting]
        public string EverythingCommandLineExePath { get; set; }
    }
}