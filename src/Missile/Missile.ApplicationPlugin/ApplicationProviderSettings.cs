using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using Missile.TextLauncher;

namespace Missile.ApplicationPlugin
{
    /// <inheritdoc />
    /// <summary>
    ///     Settings for ApplicationProvider
    /// </summary>
    /// <seealso cref="T:Missile.TextLauncher.ISettings" />
    [Export(typeof(ISettings))]
    [Serializable]
    public class ApplicationProviderSettings : ISettings
    {
        /// <summary>
        ///     Gets or sets the search paths for applications
        /// </summary>
        /// <value>
        ///     The search paths for the applications
        /// </value>
        [Setting]
        public List<string> SearchPaths { get; set; } = new List<string>();
    }
}