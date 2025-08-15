#if ABSENT_ATTRIBUTES
using com.absence.attributes;
#endif
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace com.absence.utilities.extensiblecomponents
{
    public abstract class ExtensibleComponentBase<T> : MonoBehaviour, IExtensibleComponent<T>
    {
        [SerializeField]
#if ABSENT_ATTRIBUTES
        [Readonly]
#endif
        protected List<ComponentExtensionBase<T>> m_extensionList;

        public List<ComponentExtensionBase<T>> Extensions => m_extensionList;

#if ABSENT_ATTRIBUTES
        [Button("Refresh Extension List")]
#endif
        public void Refresh()
        {
            m_extensionList = GetComponents<ComponentExtensionBase<T>>()
                .OrderBy(ext => ext.Order).ToList();

#if UNITY_EDITOR
            if (!Application.isPlaying)
            {
                UnityEditor.EditorUtility.SetDirty(this);
                UnityEditor.AssetDatabase.SaveAssetIfDirty(this);
            }
#endif
        }
    }
}
