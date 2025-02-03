using System;

namespace com.absence.utilities.experimental.databases.internals
{
    public interface IDatabaseInstance : IDisposable
    {
        int Size { get; }
        void Refresh();
        void Clear();
    }
}
