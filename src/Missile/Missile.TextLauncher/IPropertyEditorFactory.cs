using System;
using System.Windows;

namespace Missile.TextLauncher
{
    /// <summary>
    ///     Represents an object capable of providing a FrameworkElement capable of editing a particular property or field
    /// </summary>
    public interface IPropertyEditorFactory
    {
        /// <summary>
        ///     Determines whether this instance can handle the specified type
        /// </summary>
        /// <param name="type">The type</param>
        /// <returns>
        ///     <c>true</c> if this instance can handle the specified type; otherwise, <c>false</c>.
        /// </returns>
        bool CanHandle(Type type);

        /// <summary>
        ///     Gets the control for editing the specified property or field
        /// </summary>
        /// <param name="adapter">The adapter abstracting a property or field</param>
        /// <returns>A component capable of editing the specified property or field</returns>
        FrameworkElement GetControl(PropertyFieldAdapter adapter);
    }
}