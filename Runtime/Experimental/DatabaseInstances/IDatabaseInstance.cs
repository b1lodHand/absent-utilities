using com.absence.utilities.experimental.databases.internals;
using System.Collections.Generic;

namespace com.absence.utilities.experimental.databases
{
    public interface IDatabaseInstance<T1, T2> : IDatabaseInstance, IEnumerable<T2> 
        where T2 : UnityEngine.Object
    {
        T2 this[T1 key] { get; }

        public IEnumerable<T1> Keys { get; }
        public IEnumerable<T2> Values { get; }

        T2 Get(T1 key);
        bool TryGet(T1 key, out T2 output);
        bool ContainsKey(T1 key);
        bool ContainsValue(T2 value);
    }
}
