using UnityEngine;

namespace com.absence.utilities
{
    public static class Bootstrapper
    {
        public static bool ACTIVE = false;
        public static string MANAGERS_RESOURCES_PATH = "Managers";

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
        static void InstantiateManagers()
        {
            if (!ACTIVE) return;

            Object loadedManagers = Resources.Load(MANAGERS_RESOURCES_PATH);
            if (loadedManagers == null)
            {
                Debug.Log("Bootstrapper couldn't find the managers prefab.");
                return;
            }

            GameObject.Instantiate(loadedManagers);
        }
    }

}