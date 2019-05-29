using System.Collections.Generic;

namespace UtilityDoctor
{
    public abstract class Qualifier
    {
        public ActionBase action;
        public List<Scorer> scorers = new List<Scorer>();
        public string name;
        public string description;

        public float Score(Blackboard blackboard)
        {
            return Score(blackboard, scorers);
        }

        public abstract float Score(Blackboard blackboard, List<Scorer> scorers);
    }
}
