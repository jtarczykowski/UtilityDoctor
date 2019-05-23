using UnityEngine;

namespace UtilityDoctor
{
    public class TestAction : ActionBase
    {
        public override void Execute(Blackboard blackboard)
        {
            Debug.Log("Test action performed");
        }
    }
}
