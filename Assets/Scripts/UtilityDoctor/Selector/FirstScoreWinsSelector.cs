using System.Collections.Generic;

namespace UtilityDoctor
{
    public sealed class FirstScoreWinsSelector : Selector
    {
        protected override Qualifier Select(Blackboard blackboard, List<Qualifier> qualifiers)
        {
            for (int i = 0; i < qualifiers.Count; ++i)
            {
                var qualifier = qualifiers[i];
                if (qualifier.Score(blackboard) > 0f)
                {
                    return qualifier;
                }
            }

            return null;
        }
    }
}