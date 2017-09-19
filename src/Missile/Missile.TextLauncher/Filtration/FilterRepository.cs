using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;

namespace Missile.TextLauncher.Filtration
{
    [Export(typeof(IFilterRepository))]
    public class FilterRepository : IFilterRepository
    {
        public IList<RegisteredFilter> RegisteredFilters { get; set; } = new List<RegisteredFilter>();

        public RegisteredFilter Get(string name)
        {
            return RegisteredFilters.Single(x => x.Name == name);
        }
    }
}