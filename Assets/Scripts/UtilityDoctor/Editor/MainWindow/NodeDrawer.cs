using AmazingNodeEditor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UtilityDoctor.Editor
{
    public class NodeDrawer
    {
        readonly GUIStyle style;

        public NodeDrawer()
        {
            style = EditorConfig.CreateDefaultNodeStyle();
        }

        public void Draw(List<NodeBase> nodes)
        {
            if(nodes == null)
            {
                return;
            }
            foreach(var node in nodes)
            {
                GUI.Box(node.rect, node.title, style);
            }
        }
    }
}
