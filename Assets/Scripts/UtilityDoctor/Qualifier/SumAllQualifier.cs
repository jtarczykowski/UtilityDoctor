using System.Collections.Generic;

namespace UtilityDoctor
{
    public sealed class SumAllQualifier : Qualifier
    {
        public override sealed float Score(Blackboard blackboard, List<Scorer> scorers)
        {
            float result = 0;
            var count = scorers.Count;
            for (var i = 0; i < count; ++i)
            {
                result += scorers[i].Score(blackboard);
            }
            return result;
        }
    }
}