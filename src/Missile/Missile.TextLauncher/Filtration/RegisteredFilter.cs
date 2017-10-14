using System;
using System.Collections.Generic;
using System.Reflection;

namespace Missile.TextLauncher.Filtration
{
    /// <summary>
    ///     Represents a filter instance that has been registered with a repository
    /// </summary>
    public class RegisteredFilter
    {
        protected internal Type Type;

        protected internal RegisteredFilter()
        {
        }

        public RegisteredFilter(IFilter filter, Type type)
        {
            FilterInstance = filter;
            Type = type;
            Name = (string) type.GetProperty("Name").GetMethod.Invoke(filter, new object[0]);
            FilterMethodInfo = type.GetMethod("Filter");
        }

        /// <summary>
        ///     Name of the filter
        /// </summary>
        /// <value>
        ///     The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        ///     Instance of the filter
        /// </summary>
        /// <value>
        ///     The filter instance.
        /// </value>
        public object FilterInstance { get; set; }

        /// <summary>
        ///     Convenience property for the filter method
        /// </summary>
        /// <value>
        ///     The filter method information.
        /// </value>
        public MethodInfo FilterMethodInfo { get; set; }

        /// <summary>
        ///     Determines whether the specified <see cref="System.Object" />, is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="System.Object" /> to compare with this instance.</param>
        /// <returns>
        ///     <c>true</c> if the specified <see cref="System.Object" /> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(object obj)
        {
            return obj is RegisteredFilter filter &&
                   Name == filter.Name &&
                   EqualityComparer<object>.Default.Equals(FilterInstance, filter.FilterInstance) &&
                   EqualityComparer<MethodInfo>.Default.Equals(FilterMethodInfo, filter.FilterMethodInfo);
        }

        /// <summary>
        ///     Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        ///     A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table.
        /// </returns>
        public override int GetHashCode()
        {
            var hashCode = 771610216;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Name);
            hashCode = hashCode * -1521134295 + EqualityComparer<object>.Default.GetHashCode(FilterInstance);
            hashCode = hashCode * -1521134295 + EqualityComparer<MethodInfo>.Default.GetHashCode(FilterMethodInfo);
            return hashCode;
        }
    }
}