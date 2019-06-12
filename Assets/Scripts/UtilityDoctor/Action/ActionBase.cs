using System.Xml.Serialization;

namespace UtilityDoctor
{
    [XmlInclude(typeof(TestAction))]
    [XmlInclude(typeof(AnotherTestAction))]
    public abstract class ActionBase : IAction
    {
        public string id;

        public virtual void Execute(Blackboard blackboard)
        {
               
        }
    }
}
