using System;
using System.ComponentModel.Composition;
using System.Windows;
using System.Windows.Controls;

namespace Missile.TextLauncher
{
    [Export(typeof(IPropertyEditorFactory))]
    public class StringPropertyEditorFactory : IPropertyEditorFactory
    {
        public bool CanHandle(Type type)
        {
            return type == typeof(string);
        }

        public FrameworkElement GetControl(PropertyFieldAdapter adapter)
        {
            var editor = new TextBox {Text = Convert.ToString(adapter.GetValue())};
            editor.TextChanged += (sender, args) => adapter.SetValue(editor.Text);
            return editor;
        }
    }
}