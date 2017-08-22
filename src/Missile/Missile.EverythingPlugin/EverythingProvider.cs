using System;
using Missile.Core;

namespace Missile.EverythingPlugin
{
    public class EverythingProvider : IProvider
    {
        public string ProivderName { get; } = "everything";
        public string Title { get; } = "Everything provider";
        public string Description { get; } = "Searches for files using the Everything application";
    }
}
