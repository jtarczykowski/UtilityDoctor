using System.Collections.Generic;

namespace UtilityDoctor
{
    public abstract class Selector
    {
        public List<Qualifier> qualifiers = new List<Qualifier>();

        public Qualifier Select(Blackboard blackboard)
        {
            return Select(blackboard, qualifiers);
        }

        protected abstract Qualifier Select(Blackboard blackboard, List<Qualifier> qualifiers);
    }
}