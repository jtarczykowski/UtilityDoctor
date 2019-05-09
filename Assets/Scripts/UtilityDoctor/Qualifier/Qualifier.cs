using System.Collections.Generic;

namespace UtilityDoctor
{
    public abstract class Qualifier
    {
        public IAction action;
        public List<Scorer> scorers = new List<Scorer>();
        public string name;

        public float Score(Blackboard blackboard)
        {
            return Score(blackboard, scorers);
        }

        public abstract float Score(Blackboard blackboard, List<Scorer> scorers);
    }
}
