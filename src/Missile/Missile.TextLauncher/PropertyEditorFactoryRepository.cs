using System;
using System.ComponentModel.Composition;
using System.Linq;

namespace Missile.TextLauncher
{
    /// <inheritdoc />
    /// <summary>
    ///     Default implementation for IPropertyeditorFactoryRepository
    /// </summary>
    /// <seealso cref="T:Missile.TextLauncher.IPropertyEditorFactoryRepository" />
    [Export(typeof(IPropertyEditorFactoryRepository))]
    public class PropertyEditorFactoryRepository : IPropertyEditorFactoryRepository
    {
        /// <summary>
        ///     Gets or sets the property editor factories
        /// </summary>
        /// <value>
        ///     The property editor factories
        /// </value>
        [ImportMany(typeof(IPropertyEditorFactory))]
        protected internal IPropertyEditorFactory[] PropertyEditorFactories { get; set; }

        /// <summary>
        ///     Gets an IPropertyEditoryFactory instance for the specified type
        /// </summary>
        /// <param name="type">The type for which an IPropertyEditoryFactory is requested</param>
        /// <returns>
        ///     An IPropertyEditorFactory instance for the specified type
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">If there is no matching factory for the specified type</exception>
        public IPropertyEditorFactory Get(Type type)
        {
            var first = PropertyEditorFactories.FirstOrDefault(x => x.CanHandle(type));
            if (first == null)
                throw new ArgumentOutOfRangeException($"{type} does not have any registered property editors");
            return first;
        }
    }
}