using System.Collections.Generic;

namespace Missile.ApplicationPlugin
{
    /// <summary>
    ///     Represents an object that can handle managing the life times of registered applications
    /// </summary>
    public interface IApplicationRepository
    {
        /// <summary>
        ///     Searches the specified search string
        /// </summary>
        /// <param name="searchString">The search string</param>
        /// <returns>The matching registered application</returns>
        IEnumerable<RegisteredApplication> Search(string searchString);
    }
}