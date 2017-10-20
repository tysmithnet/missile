using System.Collections.Generic;
using System.Windows.Controls;

namespace Missile.ListPlugin
{
    // todo: I think this needs some sort of repo 
    /// <summary>
    ///     Represents an object that is capable of return an enumeration of 0 or more MenuItems
    ///     for an enumeration of list destination items
    /// </summary>
    public interface IDestinationContextMenuProvider
    {
        /// <summary>
        ///     Gets the menu items for a given enumeration of list destination items
        ///     Implementers should return an empty enumeration instead of throwing an exception
        /// </summary>
        /// <param name="items">The items to produce MenuItems for</param>
        /// <returns>An enumeration of 0 or more MenuItems</returns>
        // todo: change to IObservable
        IEnumerable<MenuItem> GetMenuItems(IEnumerable<IListDestinationItem> items);
    }
}