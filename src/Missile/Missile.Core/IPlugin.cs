using System;

namespace Missile.Core
{
    public interface IPlugin
    {
        string Title { get; }
        string Description { get; }
    }
}
