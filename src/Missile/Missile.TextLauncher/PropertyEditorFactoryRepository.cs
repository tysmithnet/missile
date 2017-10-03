using System;
using System.ComponentModel.Composition;
using System.Linq;
using System.Windows.Controls;

namespace Missile.TextLauncher
{
    [Export(typeof(IPropertyEditorFactoryRepository))]
    public class PropertyEditorFactoryRepository : IPropertyEditorFactoryRepository
    {
        [ImportMany(typeof(IPropertyEditorFactory))]
        protected internal IPropertyEditorFactory[] PropertyEditorFactories { get; set; }

        public IPropertyEditorFactory Get(Type type)
        {
            var first = PropertyEditorFactories.FirstOrDefault(x => x.CanHandle(type));
            if (first == null)
                throw new ArgumentOutOfRangeException($"{type} does not have any registered property editors");
            return first;
        }
    }
}