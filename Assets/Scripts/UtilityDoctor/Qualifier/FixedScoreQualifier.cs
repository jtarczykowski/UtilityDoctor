using System.Collections.Generic;

namespace UtilityDoctor
{
    public sealed class FixedScoreQualifier : Qualifier
    {
        public float score;

        public FixedScoreQualifier() : this(0f){ }

        public FixedScoreQualifier(float score)
        {
            this.score = score;
        }

        public override float Score(Blackboard blackboard, List<Scorer> scorers)
        {
            return score;
        }
    }
}