namespace Missile.TextLauncher.Filtration
{
    /// <summary>
    ///     Represents a centralized location for managing filters
    /// </summary>
    public interface IFilterRepository
    {
        /// <summary>
        ///     Gets a registered filter by name
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        RegisteredFilter Get(string name);

        /// <summary>
        ///     Adds the specified filter
        /// </summary>
        /// <param name="filter">The filter.</param>
        void Add(RegisteredFilter filter);
    }
}