using System;

namespace com.absence.utilities.observables
{
    public interface IObservable
    {
        event Action OnChange;
    }
}
