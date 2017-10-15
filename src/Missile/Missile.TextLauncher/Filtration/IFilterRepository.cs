using System.Collections.Generic;

namespace Missile.TextLauncher.Filtration
{
    /// <summary>
    ///     Represents a centralized location for managing filters
    /// </summary>
    public interface IFilterRepository
    {
        // todo: remove
        IList<RegisteredFilter> RegisteredFilters { get; set; }

        RegisteredFilter Get(string name);
    }
}