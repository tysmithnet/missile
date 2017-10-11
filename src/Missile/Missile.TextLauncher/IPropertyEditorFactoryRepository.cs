using System;

namespace Missile.TextLauncher
{
    /// <summary>
    ///     Represents an object capable of managing IPropertyEditoryFactory instances
    /// </summary>
    public interface IPropertyEditorFactoryRepository
    {
        /// <summary>
        ///     Gets an IPropertyEditoryFactory instance for the specified type
        /// </summary>
        /// <param name="type">The type for which an IPropertyEditoryFactory is requested</param>
        /// <returns>An IPropertyEditorFactory instance for the specified type</returns>
        IPropertyEditorFactory Get(Type type);
    }
}