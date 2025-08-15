using System.Collections.Generic;

namespace com.absence.utilities.extensiblecomponents
{
    public interface IExtensibleComponent<T1>
    {
        List<ComponentExtensionBase<T1>> Extensions { get; }
    }
}
