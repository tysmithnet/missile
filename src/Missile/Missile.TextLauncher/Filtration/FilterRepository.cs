using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;

namespace Missile.TextLauncher.Filtration
{
    /// <summary>
    ///     Represents a central location for filters
    /// </summary>
    [Export(typeof(IFilterRepository))]
    public class FilterRepository : IFilterRepository
    {
        public IList<RegisteredFilter> RegisteredFilters { get; set; } = new List<RegisteredFilter>();

        /// <inheritdoc />
        public RegisteredFilter Get(string name)
        {
            return RegisteredFilters.Single(x => x.Name == name);
        }
    }
}