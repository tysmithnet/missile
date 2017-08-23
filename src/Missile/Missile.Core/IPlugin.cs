using System;
using System.Threading.Tasks;

namespace Missile.Core
{
    public interface IPlugin
    {
        string Title { get; }
        string Description { get; }

        Task SetupAsync();
    }
}
