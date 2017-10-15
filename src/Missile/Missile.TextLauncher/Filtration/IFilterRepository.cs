using System.Collections.Generic;

namespace Missile.TextLauncher.Filtration
{
    /// <summary>
    ///     Represents a centralized location for managing filters
    /// </summary>
    public interface IFilterRepository
    {                                                          
        RegisteredFilter Get(string name);
    }
}