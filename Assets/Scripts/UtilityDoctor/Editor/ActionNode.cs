using System;
using System.Linq;
using AmazingNodeEditor;
using UnityEditor;
using UnityEngine;

namespace UtilityDoctor.Editor
{
    public class ActionNode : Node
    {
        public ActionBase action;
        public string nodeName = "action";
        GUISkin actionSkin;

        public void Draw()
        {
            if(action != null)
            {
                var titleRect = rect;
                titleRect.height = 20f;
                titleRect.position += Vector2.down * 20f;

                GUI.Box(titleRect, action.GetType().Name, actionSkin.box);
            }
        }

        protected override void ProcessContextMenu()
        {
            GenericMenu genericMenu = new GenericMenu();
            //genericMenu.AddItem(new GUIContent(removeNodeText), false, OnClickRemoveNode);
            genericMenu.ShowAsContext();
        }

        public ActionNode(Vector2 position, Vector2 dimensions, NodeStyleInfo styleInfo,
            Action<ConnectionPointBase> onClickInPoint, Action<ConnectionPointBase> onClickOutPoint,
            Action<Node> onClickRemoveNode, string inPointId = null, string outPointId = null) :
            base(position, dimensions, styleInfo, onClickInPoint, onClickOutPoint, onClickRemoveNode, inPointId, outPointId)
        {
            actionSkin = Resources.Load("ActionNodeSkin") as GUISkin;
        }

        public ActionNode() { }
    }
}
