using System.Collections.Generic;
using System.Linq;

namespace com.absence.utilities.componentpipelines
{
    public static class ExtensionMethods
    {
        public static T Enpipe<T>(this IComponentPipeline<T> pipeline, T value, bool needsReordering = true)
        {
            IEnumerable<ComponentPipelineComponentBase<T>> pipes;
            if (needsReordering) pipes = pipeline.Pipeline.OrderBy(ext => ext.Order);
            else pipes = pipeline.Pipeline;

            T result = value;
            foreach (var pipe in pipes)
            {
                result = pipe.Enpipe(result);
            }

            return result;
        }
    }
}
