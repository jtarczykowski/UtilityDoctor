using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UtilityDoctor.Editor
{
    public partial class UtilityDoctorRenderer
    {
        private void ProcessMouseDrag(Event e)
        {
            var nodeUnderMouse = FindNodeAtPosition(e.mousePosition);
            if (nodeUnderMouse != null)
            {
                DragNode(e.delta, nodeUnderMouse);
            }
            else
            {
                OnDragAll(e.delta);
            }
        }

        protected void DragNode(Vector2 delta, NodeBase node)
        {
            node.rect.position += delta;
        }

        protected void OnDragAll(Vector2 delta)
        {
            drag = delta;

            if (window.nodes != null)
            {
                foreach (var node in window.nodes)
                {
                    DragNode(delta, node);
                }
            }

            GUI.changed = true;
        }
    }
}
