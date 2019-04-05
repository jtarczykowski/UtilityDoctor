using System.Collections.Generic;

namespace UtilityDoctor
{
    public sealed class SumOnlyScoresAboveThresholdQualifier : Qualifier
    {
        public float threshold;

        public sealed override float Score(Blackboard blackboard, List<Scorer> scorers)
        {
            float result = 0;
            var count = scorers.Count;
            for (var i = 0; i < count; ++i)
            {
                var score = scorers[i].Score(blackboard);
                if (score > threshold)
                {
                    result += score;
                }
            }
            return result;
        }
    }
}