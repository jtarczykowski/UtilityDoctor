using System.Collections.Generic;

namespace UtilityDoctor
{
    public sealed class AllOrNothingQualifier : Qualifier
    {
        public float threshold;

        public sealed override float Score(Blackboard blackboard, List<Scorer> scorers)
        {
            var num = 0f;
            var count = scorers.Count;
            for (int i = 0; i < count; i++)
            {
                var score = scorers[i].Score(blackboard);
                if (score < threshold)
                {
                    return 0f;
                }
                num += score;
            }

            return num;
        }
    }
}
