using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Windows;
using System.Windows.Controls;

namespace Missile.TextLauncher
{
    /// <inheritdoc />
    /// <summary>
    ///     Property editor factory for string lists
    /// </summary>
    /// <seealso cref="T:Missile.TextLauncher.IPropertyEditorFactory" />
    [Export(typeof(IPropertyEditorFactory))]
    public class StringListEditorFactory : IPropertyEditorFactory
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
            return typeof(IList<string>).IsAssignableFrom(type);
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
            var stackPanel = new StackPanel();

            var list = (List<string>) adapter.GetValue();
            for (var index = 0; index < list.Count; index++)
            {
                var editor = new TextBox();
                editor.Width = 200;
                editor.Text = list[index];
                var copy = index;
                editor.TextChanged += (sender, args) => list[copy] = editor.Text;
                stackPanel.Children.Add(editor);
            }
            var addButton = new Button {Content = "Add"};
            stackPanel.Children.Add(addButton);
            addButton.Click += (sender, args) =>
            {
                var save = list.Count;
                list.Add("");
                var editor = new TextBox();
                editor.Width = 200;
                editor.TextChanged += (o, eventArgs) => list[save] = editor.Text;
                stackPanel.Children.Insert(list.Count - 1, editor);
            };
            return stackPanel;
        }
    }
}