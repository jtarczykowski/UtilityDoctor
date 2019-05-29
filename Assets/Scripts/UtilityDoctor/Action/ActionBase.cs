namespace UtilityDoctor
{
    public abstract class ActionBase : IAction
    {
        public string id;

        public virtual void Execute(Blackboard blackboard)
        {
            
        }
    }
}
