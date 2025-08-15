#if ABSENT_ATTRIBUTES
using com.absence.attributes;
#endif
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace com.absence.utilities.componentpipelines
{
    public abstract class ComponentPipelineBase<T> : MonoBehaviour, IComponentPipeline<T>
    {
        [SerializeField]
#if ABSENT_ATTRIBUTES
        [Readonly]
#endif
        protected List<ComponentPipelineComponentBase<T>> m_pipeline;
        public virtual bool NeedsReordering => false;

        public List<ComponentPipelineComponentBase<T>> Pipeline => m_pipeline;

        public virtual T Enpipe(T value)
        {
            return this.Enpipe(value, NeedsReordering);
        }

#if ABSENT_ATTRIBUTES
        [Button("Refresh Pipeline")]
#endif
        public void Refresh()
        {
            m_pipeline = GetComponents<ComponentPipelineComponentBase<T>>()
                .OrderBy(pipe => pipe.Order).ToList();

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
