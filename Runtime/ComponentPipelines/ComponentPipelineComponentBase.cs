#if ABSENT_ATTRIBUTES
using com.absence.attributes;
#endif
using UnityEngine;

namespace com.absence.utilities.componentpipelines
{
    public abstract class ComponentPipelineComponentBase<T> : MonoBehaviour
    {
        [SerializeField] private int m_order;

        public int Order => m_order;

        public abstract T Enpipe(T value);
    }

    public abstract class ComponentPipelineComponentBase<T1, T2> : ComponentPipelineComponentBase<T2> where T1 : MonoBehaviour
    {
        [SerializeField]
#if ABSENT_ATTRIBUTES
        [Readonly]
#endif
        protected T1 m_target;

        public override T2 Enpipe(T2 value)
        {
            return Enpipe(m_target, value);
        }
        public abstract T2 Enpipe(T1 target, T2 context);

        private void Reset()
        {
            FetchTarget();
        }

#if ABSENT_ATTRIBUTES
        [Button("Find Target")]
#endif
        void FetchTarget()
        {
            m_target = GetComponent<T1>();

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
