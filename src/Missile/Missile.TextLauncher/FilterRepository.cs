using System.Collections.Generic;
using System.Linq;
using Missile.TextLauncher.Interpretation;

namespace Missile.TextLauncher
{
    public class FilterRepository : IFilterRepository
    {
        public IList<RegisteredFilter> RegisteredFilters { get; set; } = new List<RegisteredFilter>();

        public RegisteredFilter Get(string name)
        {
            return RegisteredFilters.Single(x => x.Name == name);
        }
    }
}