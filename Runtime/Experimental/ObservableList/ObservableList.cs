using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

namespace com.absence.utilities.experimental
{
    [System.Serializable]
    public class ObservableList : IObservableList
    {
        public event Action OnChange = delegate { };
        public event Action<int, int> OnCapacityChanged = delegate { };
        public event Action OnContentChanged = delegate { };
        public event Action OnArrangementChanged = delegate { };
        public event Action<int> OnElementValueChanged = delegate { };

        internal void InvokeOnChange()
        {
            OnChange?.Invoke();
        }
        internal void InvokeOnElementValueChange(int index)
        {
            OnElementValueChanged?.Invoke(index);
            InvokeOnChange();
        }
        internal void InvokeOnContentChange()
        {
            OnContentChanged?.Invoke();
            InvokeOnChange();
        }
        internal void InvokeOnArrangementChange()
        {
            OnArrangementChanged?.Invoke();
            InvokeOnChange();
        }
        internal void InvokeOnCapacityChange(int oldCapacity, int newCapacity)
        {
            if (oldCapacity == newCapacity)
                return;

            OnCapacityChanged?.Invoke(oldCapacity, newCapacity);
            InvokeOnChange();
        }
    }

    [System.Serializable]
    public class ObservableList<T> : ObservableList, IObservableList<T>
    {
        [SerializeField]
        private List<T> m_internalList;

        private static readonly List<T> s_emptyList = new List<T>();

        public event Action<T> OnElementAdded = delegate { };
        public event Action<T> OnElementRemoved = delegate { };

        internal void InvokeOnElementAdd(T elementAdded)
        {
            OnElementAdded?.Invoke(elementAdded);
            InvokeOnContentChange();
        }
        internal void InvokeOnElementRemove(T elementRemoved)
        {
            OnElementRemoved?.Invoke(elementRemoved);
            InvokeOnContentChange();
        }

        public ObservableList()
        {
            m_internalList = s_emptyList;
        }

        public ObservableList(int capacity)
        {
            m_internalList = new List<T>(capacity);
        }

        public ObservableList(IEnumerable<T> collection)
        {
            m_internalList = new List<T>(collection);
        }

        public T this[int index]
        {
            get => m_internalList[index];
            set
            {
                m_internalList[index] = value;
                InvokeOnElementValueChange(index);
            }
        }

        object IList.this[int index]
        {
            get => this[index];
            set => this[index] = (T)value!;
        }

        public int Capacity
        {
            get => m_internalList.Capacity;
            set
            {
                int oldCapacity = m_internalList.Capacity;
                try
                {
                    m_internalList.Capacity = value;
                    InvokeOnCapacityChange(oldCapacity, value);
                }

                catch (Exception e)
                {
                    throw e;
                }
            }
        }

        public int Count => m_internalList.Count;

        public bool IsReadOnly => false;

        public bool IsFixedSize => false;

        public bool IsSynchronized => false;

        public object SyncRoot => this;

        public void Add(T item)
        {
            try
            {
                m_internalList.Add(item);
                InvokeOnElementAdd(item);
            }

            catch (Exception e)
            {
                throw e;
            }
        }

        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int Add(object value)
        {
            try
            {
                Add((T)value);
            }
            catch (InvalidCastException e)
            {
                throw e;
            }

            return Count - 1;
        }

        public void AddRange(IEnumerable<T> collection)
        {
            try
            {
                m_internalList.AddRange(collection);
                InvokeOnContentChange();
            }

            catch (Exception e)
            {
                throw e;
            }
        }

        public ReadOnlyCollection<T> AsReadOnly()
            => m_internalList.AsReadOnly();

        public int BinarySearch(int index, int count, T item, IComparer<T> comparer)
        {
            return m_internalList.BinarySearch(index, count, item, comparer);
        }

        public int BinarySearch(T item)
            => BinarySearch(0, Count, item, null);

        public int BinarySearch(T item, IComparer<T> comparer)
            => BinarySearch(0, Count, item, comparer);

        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Clear()
        {
            m_internalList.Clear();
            InvokeOnContentChange();
        }

        public bool Contains(T item)
        {
            return m_internalList.Contains(item);
        }

        public bool Contains(object value)
        {
            return Contains((T)value);
        }

        public ObservableList<TOutput> ConvertAll<TOutput>(Converter<T, TOutput> converter)
        {
            return new ObservableList<TOutput>(m_internalList.ConvertAll(converter));
        }

        public void CopyTo(T[] array)
            => CopyTo(array, 0);

        public void CopyTo(Array array, int index)
        {
            CopyTo(array, index);
        }

        public void CopyTo(int index, T[] array, int arrayIndex, int count)
        {
            m_internalList.CopyTo(index, array, arrayIndex, count);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            m_internalList.CopyTo(array, arrayIndex);
        }

        // public int EnsureCapacity(int capacity) { }

        public bool Exists(Predicate<T> match)
                    => m_internalList.Exists(match);

        public T Find(Predicate<T> match)
        {
            return m_internalList.Find(match);
        }

        public ObservableList<T> FindAll(Predicate<T> match)
        {
            return new ObservableList<T>(m_internalList.FindAll(match));
        }

        public int FindIndex(Predicate<T> match)
            => m_internalList.FindIndex(match);

        public int FindIndex(int startIndex, Predicate<T> match)
            => m_internalList.FindIndex(startIndex, match);

        public int FindIndex(int startIndex, int count, Predicate<T> match)
        {
            return m_internalList.FindIndex(startIndex, count, match);
        }

        public T FindLast(Predicate<T> match)
        {
            return m_internalList.FindLast(match);
        }

        public int FindLastIndex(Predicate<T> match)
            => m_internalList.FindLastIndex(match);

        public int FindLastIndex(int startIndex, Predicate<T> match)
            => m_internalList.FindLastIndex(startIndex, match);

        public int FindLastIndex(int startIndex, int count, Predicate<T> match)
        {
            return m_internalList.FindLastIndex(startIndex, count, match);
        }

        public void ForEach(Action<T> action)
        {
            m_internalList.ForEach(action);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return m_internalList.GetEnumerator();
        }

        public ObservableList<T> GetRange(int index, int count)
        {
            return new ObservableList<T>(m_internalList.GetRange(index, count));
        }

        public ObservableList<T> Slice(int start, int length)
            => new ObservableList<T>(m_internalList.GetRange(start, length));

        public int IndexOf(T item)
        {
            return m_internalList.IndexOf(item);
        }

        public int IndexOf(object value)
        {
            return IndexOf((T)value);
        }

        public int IndexOf(T item, int index)
        {
            return m_internalList.IndexOf(item, index);
        }

        public int IndexOf(T item, int index, int count)
        {
            return m_internalList.IndexOf(item, index, count);
        }

        public void Insert(int index, T item)
        {
            try
            {
                m_internalList.Insert(index, item);
                InvokeOnContentChange();
            }

            catch (Exception e)
            {
                throw e;
            }
        }

        public void Insert(int index, object value)
        {
            Insert(index, (T)value);
        }

        public void InsertRange(int index, IEnumerable<T> collection)
        {
            try
            {
                m_internalList.InsertRange(index, collection);
                InvokeOnContentChange();
            }

            catch (Exception e)
            {
                throw e;
            }
        }

        public int LastIndexOf(T item)
        {
            return m_internalList.LastIndexOf(item);
        }

        public int LastIndexOf(T item, int index)
        {
            return m_internalList.LastIndexOf(item, index);
        }

        public int LastIndexOf(T item, int index, int count)
        {
            return m_internalList.LastIndexOf(item, index, count);
        }

        public bool Remove(T item)
        {
            bool result = m_internalList.Remove(item);

            if (result) InvokeOnElementRemove(item);

            return result;
        }

        public void Remove(object value)
        {
            Remove((T)value);
        }

        public int RemoveAll(Predicate<T> match)
        {
            try
            {
                int elementsRemoved = m_internalList.RemoveAll(match);
                if (elementsRemoved > 0) InvokeOnContentChange();
                return elementsRemoved;
            }

            catch (Exception e)
            {
                throw e;
            }
        }

        public void RemoveAt(int index)
        {
            try
            {
                T elem = m_internalList[index];
                m_internalList.RemoveAt(index);
                InvokeOnElementRemove(elem);
            }

            catch (Exception e)
            {
                throw e;
            }
        }

        public void RemoveRange(int index, int count)
        {
            try
            {
                m_internalList.RemoveRange(index, count);
                InvokeOnContentChange();
            }

            catch (Exception e)
            {
                throw e;
            }

        }

        public void Reverse()
        {
            m_internalList.Reverse();
            InvokeOnArrangementChange();
        }

        public void Reverse(int index, int count)
        {
            m_internalList.Reverse(index, count);
            InvokeOnArrangementChange();
        }

        public void Sort()
        {
            m_internalList.Sort();
            InvokeOnArrangementChange();
        }

        public void Sort(IComparer<T> comparer)
        {
            m_internalList.Sort(comparer);
            InvokeOnArrangementChange();
        }

        public void Sort(int index, int count, IComparer<T> comparer)
        {
            m_internalList.Sort(index, count, comparer);
            InvokeOnArrangementChange();
        }

        public void Sort(Comparison<T> comparison)
        {
            m_internalList.Sort(comparison);
            InvokeOnArrangementChange();
        }

        public T[] ToArray()
        {
            return m_internalList.ToArray();
        }

        public void TrimExcess()
        {
            int oldCapacity = m_internalList.Capacity;
            m_internalList.TrimExcess();
            int newCapacity = m_internalList.Capacity;

            InvokeOnCapacityChange(oldCapacity, newCapacity);
        }

        public bool TrueForAll(Predicate<T> match)
        {
            return m_internalList.TrueForAll(match);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
