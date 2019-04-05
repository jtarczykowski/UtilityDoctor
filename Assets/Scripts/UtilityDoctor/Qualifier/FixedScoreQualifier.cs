using System.Collections.Generic;

namespace UtilityDoctor
{
    public sealed class FixedScoreQualifier : Qualifier
    {
        public float score;

        public FixedScoreQualifier(float score = 0)
        {
            this.score = score;
        }

        public override float Score(Blackboard blackboard, List<Scorer> scorers)
        {
            return score;
        }
    }
}