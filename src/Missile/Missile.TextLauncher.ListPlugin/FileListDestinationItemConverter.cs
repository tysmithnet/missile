using System;
using System.ComponentModel.Composition;
using System.IO;
using System.Reactive.Linq;
using Missile.TextLauncher.Conversion;

namespace Missile.TextLauncher.ListPlugin
{
    /// <inheritdoc />
    /// <summary>
    ///     Converter capable of transforming FileInfo into FileListDestinationItem
    /// </summary>
    /// <seealso
    ///     cref="!:Missile.TextLauncher.Conversion.IConverter{System.IO.FileInfo, Missile.TextLauncher.ListPlugin.FileListDestinationItem}" />
    [Export(typeof(IConverter))]
    public class FileListDestinationItemConverter : IConverter<FileInfo, FileListDestinationItem>
    {
        /// <inheritdoc />
        /// <summary>
        ///     Converts the specified source of FileInfo into FileListDestinationItem
        /// </summary>
        /// <param name="source">The source</param>
        /// <returns></returns>
        public IObservable<FileListDestinationItem> Convert(IObservable<FileInfo> source)
        {
            return source.Select(f => new FileListDestinationItem(f));
        }
    }
}