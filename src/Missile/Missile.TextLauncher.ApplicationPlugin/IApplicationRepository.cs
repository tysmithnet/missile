using System.Collections.Generic;
using System.IO;

namespace Missile.TextLauncher.ApplicationPlugin
{
    public interface IApplicationRepository
    {
        IEnumerable<RegisteredApplication> Search(string searchString);
    }
}