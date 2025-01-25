using System.Collections.Generic;

namespace com.absence.utilities.experimental.observablelists
{
    public static class ExtensionMethods
    {
        public static ObservableList<T> ToObservableList<T>(this IEnumerable<T> enumerable)
        {
            return new ObservableList<T>(enumerable);
        }
    }
}
