using System;
using System.Collections.Generic;
using UnityEngine;

namespace com.absence.utilities.experimental.databases
{
    public abstract class ResourcesDatabaseInstanceBase<T1, T2> : DatabaseInstanceBase<T1, T2> where T2 : UnityEngine.Object
    {
        protected string m_path;
        protected bool m_workOnOriginals;

        public event Action OnRefreshComplete;

        public ResourcesDatabaseInstanceBase(string path, bool workOnOriginals = true) : base()
        {
            m_path = path;
            m_workOnOriginals = workOnOriginals;
            OnRefreshComplete = null;
        }

        public override void Clear()
        {
            if (!m_workOnOriginals)
            {
                base.Clear();
                return;
            }

            foreach (KeyValuePair<T1, T2> entry in Data)
            {
                Resources.UnloadAsset(entry.Value);
            }

            base.Clear();
        }

        public override void Refresh()
        {
            Clear();

            T2[] objectsLoaded = Resources.LoadAll<T2>(m_path);
            Fetch(ref objectsLoaded);
        }

        public override void Dispose()
        {
            base.Dispose();
            Resources.UnloadUnusedAssets();
        }

        void Fetch(ref T2[] loadedOriginals)
        {
            if (m_workOnOriginals)
            {
                foreach (T2 original in loadedOriginals)
                {
                    if (!TryGenerateKey(original, out T1 key))
                        continue;

                    Data.Add(key, original);
                }
            }

            else
            {
                foreach (T2 clone in CloneOneByOne(loadedOriginals))
                {
                    if (!TryGenerateKey(clone, out T1 key))
                        continue;

                    Data.Add(key, clone);
                }
            }

            loadedOriginals = null;

            Resources.UnloadUnusedAssets();
            OnRefreshComplete?.Invoke();
        }
        IEnumerable<T2> CloneOneByOne(IEnumerable<T2> targets)
        {
            foreach (T2 target in targets)
            {
                T2 clone = UnityEngine.Object.Instantiate(target);
                Resources.UnloadAsset(target);
                yield return clone;
            }
        }
    }
    public class ResourcesDatabaseInstance<T1, T2> : ResourcesDatabaseInstanceBase<T1, T2> where T2 : UnityEngine.Object, IDatabaseMember<T1>
    {
        public ResourcesDatabaseInstance(string path, bool workOnOriginals = true) : base(path, workOnOriginals)
        {
        }

        protected override bool TryGenerateKey(T2 target, out T1 output)
        {
            output = target.GetDatabaseKey();
            return true;
        }
    }
}
