using System.Collections.Generic;

namespace Missile.TextLauncher.ApplicationPlugin
{
    public interface IApplicationRepository
    {
        IEnumerable<RegisteredApplication> Search(string searchString);
    }
}