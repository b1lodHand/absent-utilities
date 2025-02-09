using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace com.absence.utilities.experimental.databases
{
    public abstract class AddressablesDatabaseInstanceBase<T1, T2> : DatabaseInstanceBase<T1, T2>, IAsyncDatabaseInstance<T2> where T2 : UnityEngine.Object
    {
        IEnumerable m_keys;
        object m_key;
        Addressables.MergeMode m_mergeMode;
        bool m_workOnOriginals;
        bool m_readyForNextRefresh;
        bool m_singleTag;
        AsyncOperationHandle<IList<T2>>? m_handle;

        public AsyncOperationHandle<IList<T2>>? OpHandle => m_handle;
        public event Action<bool> OnRefreshComplete;

        public AddressablesDatabaseInstanceBase(object key, bool workOnOriginals) : base()
        {
            m_key = key;
            m_workOnOriginals = workOnOriginals;
            m_readyForNextRefresh = true;
            m_singleTag = true;
            OnRefreshComplete = null;
        }

        public AddressablesDatabaseInstanceBase(IList<object> keys, Addressables.MergeMode mergeMode, bool workOnOriginals = true) : base()
        {
            m_keys = new List<object>(keys);
            m_mergeMode = mergeMode;
            m_workOnOriginals = workOnOriginals;
            m_readyForNextRefresh = true;
            m_singleTag = false;
            OnRefreshComplete = null;
        }

        public override void Refresh()
        {
            bool startedSuccessfully = RefreshAsync();
            if (startedSuccessfully) m_handle.Value.WaitForCompletion();
        }

        public override void Clear()
        {
            if (m_workOnOriginals)
            {
                m_handle.Value.Release();
                m_handle = null;
            }

            base.Clear();
        }

        public bool RefreshAsync()
        {
            if (!m_readyForNextRefresh)
                return false;

            if (m_singleTag) m_handle = Addressables.LoadAssetsAsync<T2>(m_key, null);
            else m_handle = Addressables.LoadAssetsAsync<T2>(m_keys, null, m_mergeMode);

            m_handle.Value.Completed += OnHandleCompleted;

            return true;
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

                m_handle.Value.Release();
            }

            loadedOriginals = null;
            OnRefreshComplete?.Invoke(true);
        }

        IEnumerable<T2> CloneOneByOne(IEnumerable<T2> targets)
        {
            foreach (T2 target in targets)
            {
                T2 clone = UnityEngine.Object.Instantiate(target);
                yield return clone;
            }
        }

        private void OnHandleCompleted(AsyncOperationHandle<IList<T2>> handle)
        {
            if (handle.Status == AsyncOperationStatus.Failed)
            {
                OnRefreshComplete?.Invoke(false);
            }

            else if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                Clear();

                T2[] result = handle.Result.ToArray();
                Fetch(ref result);

                OnRefreshComplete?.Invoke(true);
            }

            m_handle = null;
            m_readyForNextRefresh = true;
        }
    }
    public class AddressablesDatabaseInstance<T1, T2> : AddressablesDatabaseInstanceBase<T1, T2> where T2 : UnityEngine.Object, IDatabaseMember<T1>
    {
        public AddressablesDatabaseInstance(object key, bool workOnOriginals = true) : base(key, workOnOriginals)
        {
        }

        public AddressablesDatabaseInstance(IList<object> keys, Addressables.MergeMode mergeMode, bool workOnOriginals = true) : base(keys, mergeMode, workOnOriginals)
        {

        }

        protected override bool TryGenerateKey(T2 target, out T1 output)
        {
            output = target.GetDatabaseKey();
            return true;
        }
    }
}
