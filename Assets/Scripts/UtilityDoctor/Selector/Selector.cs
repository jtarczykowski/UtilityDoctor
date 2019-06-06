using System.Collections.Generic;
using System.Xml.Serialization;

namespace UtilityDoctor
{
    [XmlInclude(typeof(HighestScoreWinsSelector))]
    [XmlInclude(typeof(FirstScoreWinsSelector))]
    public abstract class Selector
    {
        public string id;
        public List<Qualifier> qualifiers = new List<Qualifier>();

        public Qualifier Select(Blackboard blackboard)
        {
            return Select(blackboard, qualifiers);
        }

        protected abstract Qualifier Select(Blackboard blackboard, List<Qualifier> qualifiers);
    }
}