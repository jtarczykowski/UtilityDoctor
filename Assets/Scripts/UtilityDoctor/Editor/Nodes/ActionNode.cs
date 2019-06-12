using System;
using System.Linq;
using AmazingNodeEditor;
using UnityEditor;
using UnityEngine;

namespace UtilityDoctor.Editor
{
    public class ActionNode : NodeBase
    {
        public ActionBase action;
        public string nodeName = "action";
        GUISkin actionSkin;

        public ActionNode(ActionBase action,Vector2 position, Vector2 dimensions, string id = null) :
            base(position, dimensions, id)
        {
            this.action = action;
            Init();
        }

        private void Init()
        {
            actionSkin = Resources.Load("ActionNodeSkin") as GUISkin;
        }

        public ActionNode() { }
    }
}
