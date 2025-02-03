using com.absence.utilities.observables;
using System;
using System.Collections;
using System.Collections.Generic;

namespace com.absence.utilities.experimental.observablelists
{
    public interface IObservableList : IObservable
    {
        event Action OnContentChanged;
        event Action OnArrangementChanged;
        event Action<int> OnElementValueChanged;
        event Action<int, int> OnCapacityChanged;
    }

    public interface IObservableList<T> : IList, IList<T>, IReadOnlyList<T>
    {
        event Action<T> OnElementAdded;
        event Action<T> OnElementRemoved;
    }
}
