using System;

namespace com.absence.utilities.experimental.databases
{
    public interface IAsyncDatabaseInstance<T2> where T2 : UnityEngine.Object
    {
        public event Action<bool> OnRefreshComplete;
        bool RefreshAsync();
    }
}
