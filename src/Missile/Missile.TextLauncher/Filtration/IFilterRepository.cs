using System.Collections.Generic;

namespace Missile.TextLauncher.Filtration
{
    public interface IFilterRepository
    {
        IList<RegisteredFilter> RegisteredFilters { get; set; }
        RegisteredFilter Get(string name);
    }
}