using System;
using System.Collections.Generic;
using System.Linq;

namespace Missile.TextLauncher.Interpretation
{
    public interface IFilterRepository
    {
        IList<RegisteredFilter> RegisteredFilters { get; set; }
        RegisteredFilter Get(string name);
    }

    public class FilterRepository : IFilterRepository
    {
        public IList<RegisteredFilter> RegisteredFilters { get; set; }
        public RegisteredFilter Get(string name)
        {
            return RegisteredFilters.Single(x => x.Name == name);
        }
    }
}