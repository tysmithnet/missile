using System;
using System.ComponentModel.Composition;
using System.IO;
using System.Reactive.Linq;
using Missile.TextLauncher.Conversion;

namespace Missile.TextLauncher.ListPlugin
{
    [Export(typeof(IConverter))]
    public class FileListDestinationItemConverter : IConverter<FileInfo, FileListDestinationItem>
    {
        [ImportMany]
        private IDestinationContextMenuProvider<FileInfo>[] FileInfoContextMenuProviders { get; set; }

        public IObservable<FileListDestinationItem> Convert(IObservable<FileInfo> source)
        {
            return source.Select(f => new FileListDestinationItem(f, FileInfoContextMenuProviders));
        }
    }
}