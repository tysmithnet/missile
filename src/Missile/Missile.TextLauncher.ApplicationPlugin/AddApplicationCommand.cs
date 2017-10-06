using System;
using System.IO;

namespace Missile.TextLauncher.ApplicationPlugin
{
    public class AddApplicationCommand : ICommand
    {
        public AddApplicationCommand(FileInfo item)
        {
            FileInfo = item;
        }

        public FileInfo FileInfo { get; set; }

        public Guid Id { get; } = Guid.NewGuid();
    }
}