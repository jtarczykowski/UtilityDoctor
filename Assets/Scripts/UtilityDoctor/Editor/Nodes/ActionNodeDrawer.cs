using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace UtilityDoctor.Editor
{
    public class ActionNodeDrawer
    {
        readonly GUISkin actionSkin;
        private NodeDrawer nodeDrawer;
        private UtilityDoctorEditor window;

        public ActionNodeDrawer(UtilityDoctorEditor window)
        {
            actionSkin = Resources.Load("DoctorGUISkin") as GUISkin;
            nodeDrawer = new NodeDrawer();
            this.window = window;
        }

        public void Draw(List<ActionNode> nodes)
        {
            if (nodes == null)
            {
                return;
            }

            nodeDrawer.Draw(nodes.Select(n => n as NodeBase).ToList());

            foreach (var node in nodes)
            {
                var titleRect = node.rect;
                titleRect.height = 20f;
                titleRect.position += Vector2.down * 20f;
                GUI.Box(titleRect, node.action.GetType().Name, actionSkin.box);
            }
        }

        public void ProcessContextMenu(ActionNode actionNode)
        {
            
        }
    }
}
