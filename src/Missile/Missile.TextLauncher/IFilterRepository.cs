using System.Collections.Generic;
using Missile.TextLauncher.Interpretation;

namespace Missile.TextLauncher
{
    public interface IFilterRepository
    {
        IList<RegisteredFilter> RegisteredFilters { get; set; }
        RegisteredFilter Get(string name);
    }
}