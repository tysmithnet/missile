using System;
using System.IO;

namespace Missile.TextLauncher.ApplicationPlugin
{
    public class AddApplicationCommand : ICommand
    {
        public FileInfo FileInfo { get; set; }

        public AddApplicationCommand(FileInfo item)
        {
            FileInfo = item;
        }

        public Guid Id { get; } = Guid.NewGuid();
    }
}