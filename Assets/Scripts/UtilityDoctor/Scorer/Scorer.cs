namespace UtilityDoctor
{
    public abstract class Scorer
    {
        public string id;
        public string ScorerName;
        public abstract float Score(Blackboard blackboard);
    }
}
