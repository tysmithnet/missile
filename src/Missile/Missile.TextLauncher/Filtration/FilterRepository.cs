using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;

namespace Missile.TextLauncher.Filtration
{
    /// <inheritdoc />
    /// <summary>
    /// Represents a central location for filters
    /// </summary>
    /// <seealso cref="T:Missile.TextLauncher.Filtration.IFilterRepository" />
    [Export(typeof(IFilterRepository))]
    public class FilterRepository : IFilterRepository
    {
        /// <summary>
        /// Gets or sets the registered filters.
        /// </summary>
        /// <value>
        /// The registered filters.
        /// </value>
        public IList<RegisteredFilter> RegisteredFilters { get; set; } = new List<RegisteredFilter>();

        /// <summary>
        /// Gets the specified name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        /// <inheritdoc />
        public RegisteredFilter Get(string name)
        {
            return RegisteredFilters.Single(x => x.Name == name);
        }
    }
}