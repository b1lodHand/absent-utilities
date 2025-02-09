using com.absence.utilities.experimental.databases.internals;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace com.absence.utilities.experimental.databases
{
    public static class Databases
    {
        static Dictionary<string, IDatabaseInstance> s_instances;

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterAssembliesLoaded)]
        static void Initialize()
        {
            s_instances = new Dictionary<string, IDatabaseInstance>();
            Application.quitting -= OnApplicationQuit;
            Application.quitting += OnApplicationQuit;
        }

        private static void OnApplicationQuit()
        {
            foreach (KeyValuePair<string, IDatabaseInstance> pair in s_instances)
            {
                pair.Value.Dispose();
            }
        }

        public static IDatabaseInstance<T1, T2> Get<T1, T2>(string identifier) where T2 : UnityEngine.Object
        {
            return s_instances[identifier] as IDatabaseInstance<T1, T2>;
        }

        public static bool TryGet<T1, T2>(string identifier, out IDatabaseInstance<T1, T2> output) where T2 : UnityEngine.Object
        {
            output = null;
            bool result = s_instances.TryGetValue(identifier, out IDatabaseInstance rawOutput);
            if (result) output = rawOutput as IDatabaseInstance<T1, T2>;
            return result;
        }

        public static void Add<T1, T2>(string key, IDatabaseInstance<T1, T2> value) where T2 : UnityEngine.Object
        {
            s_instances.Add(key, value);
        }

        public static void Remove(string key)
        {
            s_instances.Remove(key);
        }

        public static void Remove<T1, T2>(IDatabaseInstance<T1, T2> value) where T2 : UnityEngine.Object
        {
            string key = s_instances.Keys.
                FirstOrDefault(k => (s_instances[k] as IDatabaseInstance<T1, T2>).Equals(value));

            if (key != null)
                s_instances.Remove(key);
        }

        public static bool Contains(string key)
        {
            return s_instances.ContainsKey(key);
        }

        public static bool Contains<T1, T2>(IDatabaseInstance<T1, T2> value) where T2 : UnityEngine.Object
        {
            return s_instances.ContainsValue(value);
        }
    }
}
