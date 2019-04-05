using System.Collections.Generic;

namespace UtilityDoctor
{
    public sealed class SumAboveThresholdOrNothingQualifier : Qualifier
    {
        public float threshold;

        public sealed override float Score(Blackboard blackboard, List<Scorer> scorers)
        {
            float result = 0;
            var count = scorers.Count;
            for (var i = 0; i < count; ++i)
            {
                result += scorers[i].Score(blackboard);
            }

            return result > threshold
                ? result
                : 0;
        }
    }
}
