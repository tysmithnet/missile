using System.Collections.Generic;

namespace Missile.TextLauncher.Interpretation
{
    public interface IFilterRepository
    {
        IList<RegisteredFilter> RegisteredFilters { get; set; }
        RegisteredFilter Get(string name);
    }
}