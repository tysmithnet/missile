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
        ///     The registered filters
        /// </summary>
        private IList<RegisteredFilter> _registeredFilters;

        /// <summary>
        ///     Gets or sets the filter instances
        /// </summary>
        /// <value>
        ///     The filters.
        /// </value>
        [ImportMany]
        protected internal IFilter[] Filters { get; set; }

        /// <summary>
        ///     Gets or sets the registered filters.
        /// </summary>
        /// <value>
        ///     The registered filters.
        /// </value>
        protected internal IList<RegisteredFilter> RegisteredFilters =>
            _registeredFilters ?? (_registeredFilters = ExtractRegisteredFilters(Filters));

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
            _registeredFilters = _registeredFilters ?? new List<RegisteredFilter>();
            RegisteredFilters.Add(filter);
        }

        /// <summary>
        ///     Extracts the registered filters
        /// </summary>
        /// <param name="filters">The filters from which to extracts</param>
        /// <returns></returns>
        protected internal IList<RegisteredFilter> ExtractRegisteredFilters(IFilter[] filters)
        {
            var mapping = filters.Select(d => new
            {
                Instance = d,
                Interfaces = d.GetType().GetInterfaces()
                    .Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IFilter<,>)).ToList()
            }).Where(x => x.Interfaces.Any());

            return (from item in mapping
                from iface in item.Interfaces
                select new RegisteredFilter(item.Instance, iface)).ToList();
        }
    }
}