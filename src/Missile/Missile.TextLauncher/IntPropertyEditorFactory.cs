using System;
using System.ComponentModel.Composition;
using System.Diagnostics.CodeAnalysis;
using System.Windows;
using System.Windows.Controls;

namespace Missile.TextLauncher
{
    /// <inheritdoc />
    /// <summary>
    ///     Property editor factory for int
    /// </summary>
    /// <seealso cref="T:Missile.TextLauncher.IPropertyEditorFactory" />
    [Export(typeof(IPropertyEditorFactory))]
    public class IntPropertyEditorFactory : IPropertyEditorFactory       // todo: make for number types
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
            return typeof(int).IsAssignableFrom(type);
        }

        /// <inheritdoc />
        /// <summary>
        ///     Gets the control for editing the specified property or field
        /// </summary>
        /// <param name="adapter">The adapter abstracting a property or field</param>
        /// <returns>
        ///     A component capable of editing the specified property or field
        /// </returns>
        [ExcludeFromCodeCoverage] // todo: refactor into MVVM
        public FrameworkElement GetControl(PropertyFieldAdapter adapter)
        {
            var editor = new TextBox
            {
                Text = adapter.GetValue().ToString(),
                Width = 200
            };

            editor.TextChanged += (sender, args) => adapter.SetValue(Convert.ToInt32(editor.Text));

            return editor;
        }
    }
}