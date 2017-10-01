using System.Reflection;

namespace Missile.TextLauncher.Filtration
{
    /// <summary>
    /// Represents a filter instance that has been registered with a repository
    /// </summary>
    public sealed class RegisteredFilter
    {
        /// <summary>
        /// Name of the filter
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Instance of the filter
        /// </summary>
        public object FilterInstance { get; set; }

        /// <summary>
        /// Convenience property for the filter method
        /// </summary>
        public MethodInfo FilterMethodInfo { get; set; }
    }
}