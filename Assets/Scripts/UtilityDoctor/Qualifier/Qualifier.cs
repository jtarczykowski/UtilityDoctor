using System.Collections.Generic;
using System.Xml.Serialization;

namespace UtilityDoctor
{
    [XmlInclude(typeof(AllOrNothingQualifier))]
    [XmlInclude(typeof(FixedScoreQualifier))]
    [XmlInclude(typeof(SumAboveThresholdOrNothingQualifier))]
    [XmlInclude(typeof(SumAllQualifier))]
    [XmlInclude(typeof(SumOnlyScoresAboveThresholdQualifier))]
    public abstract class Qualifier
    {
        public string id;
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
