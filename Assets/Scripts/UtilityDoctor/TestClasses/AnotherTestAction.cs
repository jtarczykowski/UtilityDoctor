﻿using UnityEngine;

namespace UtilityDoctor
{
    public class AnotherTestAction : ActionBase
    {
        public override void Execute(Blackboard blackboard)
        {
            Debug.Log("Test action performed");
        }
    }
}
