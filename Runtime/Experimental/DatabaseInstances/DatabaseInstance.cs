using com.absence.utilities.experimental.databases.internals;
using System.Collections.Generic;
using System;

namespace com.absence.utilities.experimental.databases
{
    public abstract class DatabaseInstance<T1, T2> : BaseDatabaseInstance<T1, T2>
        where T2 : UnityEngine.Object
    {
        public DatabaseInstance() : base()
        {
        }
    }
    public abstract class MemberDatabaseInstance<T1, T2> : BaseDatabaseInstance<T1, T2>
        where T2 : UnityEngine.Object, IDatabaseMember<T1>
    {
        public MemberDatabaseInstance() : base()
        {
        }

        protected override bool TryGenerateKey(T2 target, out T1 output)
        {
            output = target.GetDatabaseKey();
            return true;
        }
    }
    public sealed class DynamicDatabaseInstance<T1, T2> : BaseDatabaseInstance<T1, T2>
        where T2 : UnityEngine.Object
    {
        static T1 ThrowNotSetException(T2 target)
        {
            throw new Exception("The key generator func of this DynamicDatabase instance is not set.");
        }

        Func<Dictionary<T1, T2>> m_onRefresh;
        Func<T2, T1> m_keyGenerator;

        public DynamicDatabaseInstance() : base()
        {
            m_keyGenerator = ThrowNotSetException;
        }

        public DynamicDatabaseInstance(Func<Dictionary<T1, T2>> onRefresh, Func<T2, T1> keyGenerator) : base()
        {
            if (keyGenerator != null) m_keyGenerator = keyGenerator;
            else m_keyGenerator = ThrowNotSetException;

            m_onRefresh = onRefresh;
        }

        public override void Refresh()
        {
            m_dictionary = m_onRefresh.Invoke();
        }

        protected override bool TryGenerateKey(T2 target, out T1 output)
        {
            try
            {
                output = m_keyGenerator.Invoke(target);
                return output != null;
            }

            catch
            {
                output = default(T1);
                return false;
            }
        }
    }
}