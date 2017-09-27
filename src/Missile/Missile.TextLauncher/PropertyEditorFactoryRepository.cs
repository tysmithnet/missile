using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Missile.TextLauncher
{
    [Export(typeof(IPropertyEditorFactoryRepository))]
    public class PropertyEditorFactoryRepository : IPropertyEditorFactoryRepository
    {
        [ImportMany(typeof(IPropertyEditorFactory))]
        public IPropertyEditorFactory[] PropertyEditorFactories { get; set; }

        public IPropertyEditorFactory Get(Type type)
        {
            var first = PropertyEditorFactories.FirstOrDefault(x => x.CanHandle(type));
            if(first == null)
                throw new ArgumentOutOfRangeException($"{type} does not have any registered property editors");
            return first;
        }
    }

    [Export(typeof(IPropertyEditorFactory))]
    public class StringListEditoryFactory : IPropertyEditorFactory
    {
        public bool CanHandle(Type type)
        {
            return typeof(IList<string>).IsAssignableFrom(type);
        }

        public UIElement GetControl(PropertyFieldAdapter adapter)
        {
            StackPanel stackPanel = new StackPanel();

            var list = (List<string>) adapter.GetValue();
            for (var index = 0; index < list.Count; index++)
            {
                var editor = new TextBox();
                editor.Width = 200;
                editor.Text = list[index];
                int copy = index;
                editor.TextChanged += (sender, args) => list[copy] = editor.Text;
                stackPanel.Children.Add(editor);
            }
            var addButton = new Button {Content = "Add"};
            stackPanel.Children.Add(addButton);
            addButton.Click += (sender, args) =>
            {
                int save = list.Count;
                list.Add("");
                var editor = new TextBox();
                editor.Width = 200;
                editor.TextChanged += (o, eventArgs) => list[save] = editor.Text;
                stackPanel.Children.Insert(list.Count - 1, editor);
            };
            return stackPanel;
        }
    }

    [Export(typeof(IPropertyEditorFactory))]
    public class IntEditorFactory : IPropertyEditorFactory
    {
        public bool CanHandle(Type type)
        {
            return typeof(int).IsAssignableFrom(type);
        }

        public UIElement GetControl(PropertyFieldAdapter adapter)
        {
            var editor = new TextBox();

            editor.Text = adapter.GetValue().ToString();
            editor.Width = 200;
            editor.TextChanged += (sender, args) => adapter.SetValue(Convert.ToInt32(editor.Text));

            return editor;
        }
    }
}
