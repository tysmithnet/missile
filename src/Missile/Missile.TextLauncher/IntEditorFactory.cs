using System;
using System.ComponentModel.Composition;
using System.Windows;
using System.Windows.Controls;

namespace Missile.TextLauncher
{
    [Export(typeof(IPropertyEditorFactory))]
    public class IntEditorFactory : IPropertyEditorFactory
    {
        public bool CanHandle(Type type)
        {
            return typeof(int).IsAssignableFrom(type);
        }

        public FrameworkElement GetControl(PropertyFieldAdapter adapter)
        {
            var editor = new TextBox();

            editor.Text = adapter.GetValue().ToString();
            editor.Width = 200;
            editor.TextChanged += (sender, args) => adapter.SetValue(Convert.ToInt32(editor.Text));

            return editor;
        }
    }
}