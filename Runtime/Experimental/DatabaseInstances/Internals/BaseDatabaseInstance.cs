using System;
using System.Collections;
using System.Collections.Generic;

namespace com.absence.utilities.experimental.databases.internals
{
    public abstract class BaseDatabaseInstance<T1, T2> : IDatabaseInstance<T1, T2>
        where T2 : UnityEngine.Object
    {
        protected Dictionary<T1, T2> m_dictionary;

        public virtual Dictionary<T1, T2> Data
        {
            get
            {
                return m_dictionary;
            }

            internal set
            {
                m_dictionary = value;
            }
        }
        public virtual int Size => Data.Count;

        public virtual IEnumerable<T1> Keys => Data.Keys;
        public virtual IEnumerable<T2> Values => Data.Values;

        public T2 this[T1 key]
        {
            get
            {
                return Data[key];
            }

            protected set
            {
                try
                {
                    Data[key] = value;
                }

                catch (Exception e)
                {
                    throw e;
                }
            }
        }

        public BaseDatabaseInstance()
        {
            m_dictionary = new();
        }

        public abstract void Refresh();
        protected abstract bool TryGenerateKey(T2 target, out T1 output);

        public virtual void Clear()
        {
            Data.Clear();
        }

        public virtual bool ContainsKey(T1 key)
        {
            return Data.ContainsKey(key);
        }

        public virtual bool ContainsValue(T2 value)
        {
            return Data.ContainsValue(value);
        }

        public virtual T2 Get(T1 key)
        {
            return Data[key];
        }

        public virtual bool TryGet(T1 key, out T2 output)
        {
            bool result = Data.TryGetValue(key, out output);
            return result;
        }

        public virtual void Dispose()
        {
            Clear();
            Data = null;
        }

        public virtual IEnumerator<T2> GetEnumerator()
        {
            return Data.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
