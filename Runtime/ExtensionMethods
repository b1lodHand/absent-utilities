using UnityEngine;

namespace com.absence.utilities
{
    /// <summary>
    /// Holds some yummy extension methods.
    /// </summary>
    public static class ExtensionMethods
    {
        /// <summary>
        /// Destroy all child objects of this transform
        /// Usage:
        /// <code>
        /// transform.DestroyChildren();
        /// </code>
        /// </summary>
        public static void DestroyChildren(this Transform t)
        {
            foreach (Transform child in t) Object.Destroy(child.gameObject);
        }
    }

}
