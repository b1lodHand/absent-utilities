using UnityEngine;

namespace com.absence.utilities
{
    public static class Bootstrapper
    {
        const string MANAGERS_RESOURCES_PATH = "Managers";

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
        static void InstantiateManagers()
        {
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