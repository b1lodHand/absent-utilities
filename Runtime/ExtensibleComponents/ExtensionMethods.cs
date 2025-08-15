using System.Collections.Generic;
using System.Linq;

namespace com.absence.utilities.extensiblecomponents
{
    public static class ExtensionMethods
    {
        public static void RunExtensionsWithContext<T>(this IExtensibleComponent<T> extensibleComponent, T context, bool needsReordering = true)
        {
            IEnumerable<ComponentExtensionBase<T>> extensions;
            if (needsReordering) extensions = extensibleComponent.Extensions.OrderBy(ext => ext.Order);
            else extensions = extensibleComponent.Extensions;

            foreach (var extension in extensions)
            {
                extension.Apply(context);
            }
        }
    }
}
