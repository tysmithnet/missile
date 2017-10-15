using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;

namespace Missile.TextLauncher.Filtration
{
    /// <inheritdoc />
    /// <summary>
    ///     Represents a central location for filters
    /// </summary>
    /// <seealso cref="T:Missile.TextLauncher.Filtration.IFilterRepository" />
    [Export(typeof(IFilterRepository))]
    public class FilterRepository : IFilterRepository
    {
        /// <summary>
        ///     Gets or sets the registered filters.
        /// </summary>
        /// <value>
        ///     The registered filters.
        /// </value>
        protected internal IList<RegisteredFilter> RegisteredFilters { get; set; } = new List<RegisteredFilter>();

        /// <summary>
        ///     Gets the specified name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        /// <inheritdoc />
        public RegisteredFilter Get(string name)
        {
            return RegisteredFilters.Single(x => x.Name == name);
        }

        /// <inheritdoc />
        /// <summary>
        ///     Adds the specified filter
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <exception cref="T:System.NotImplementedException"></exception>
        public void Add(RegisteredFilter filter)
        {
            RegisteredFilters.Add(filter);
        }
    }
}