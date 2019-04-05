using System.Collections.Generic;

namespace UtilityDoctor
{
    public sealed class HighestScoreWinsSelector : Selector
    {
        protected override Qualifier Select(Blackboard blackboard, List<Qualifier> qualifiers)
        {
            var max = float.NegativeInfinity;
            Qualifier currentMax = null;

            for (int i = 0; i < qualifiers.Count; ++i)
            {
                var qualifier = qualifiers[i];
                var qualifierScore = qualifier.Score(blackboard);
                if (qualifierScore > max)
                {
                    currentMax = qualifier;
                    max = qualifierScore;
                }
            }

            return currentMax;
        }
    }
}