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
        /// <summary>
        /// The type
        /// </summary>
        protected internal Type Type;

        /// <summary>
        /// Initializes a new instance of the <see cref="RegisteredFilter"/> class.
        /// </summary>
        protected internal RegisteredFilter()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RegisteredFilter"/> class.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <param name="type">The type.</param>
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
        /// Invokes the filter method from the filter
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <param name="observable">The observable.</param>
        /// <returns></returns>
        public object Filter(string[] args, object observable)
        {
            return FilterMethodInfo.Invoke(FilterInstance, new [] {args, observable});
        }

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