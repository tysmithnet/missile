using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Windows;
using System.Windows.Controls;

namespace Missile.TextLauncher
{
    [Export(typeof(IPropertyEditorFactory))]
    public class StringListEditoryFactory : IPropertyEditorFactory
    {
        public bool CanHandle(Type type)
        {
            return typeof(IList<string>).IsAssignableFrom(type);
        }

        public UIElement GetControl(PropertyFieldAdapter adapter)
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