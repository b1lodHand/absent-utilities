using System.Collections.Generic;

namespace com.absence.utilities.componentpipelines
{
    public interface IComponentPipeline<T>
    {
        List<ComponentPipelineComponentBase<T>> Pipeline { get; }
    }
}
