using System;
using System.ComponentModel.Composition;
using System.Windows;
using System.Windows.Controls;
using Missile.Core;

namespace Missile.TextLauncher
{
    /// <inheritdoc />
    /// <summary>
    ///     Property editor factory for strings
    /// </summary>
    /// <seealso cref="T:Missile.TextLauncher.IPropertyEditorFactory" />
    [Export(typeof(IPropertyEditorFactory))]
    public class StringPropertyEditorFactory : IPropertyEditorFactory
    {
        /// <inheritdoc />
        /// <summary>
        ///     Determines whether this instance can handle the specified type
        /// </summary>
        /// <param name="type">The type</param>
        /// <returns>
        ///     <c>true</c> if this instance can handle the specified type; otherwise, <c>false</c>.
        /// </returns>
        public bool CanHandle(Type type)
        {
            return type == typeof(string);
        }

        /// <inheritdoc />
        /// <summary>
        ///     Gets the control for editing the specified property or field
        /// </summary>
        /// <param name="adapter">The adapter abstracting a property or field</param>
        /// <returns>
        ///     A component capable of editing the specified property or field
        /// </returns>
        public FrameworkElement GetControl(PropertyFieldAdapter adapter)
        {
            var editor = new TextBox {Text = Convert.ToString(adapter.GetValue())};
            editor.TextChanged += (sender, args) => adapter.SetValue(editor.Text);
            return editor;
        }
    }
}