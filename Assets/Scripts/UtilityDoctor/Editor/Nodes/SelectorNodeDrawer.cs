using AmazingNodeEditor;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace UtilityDoctor.Editor
{
    public class SelectorNodeDrawer
    {
        readonly GUISkin selectorSkin;
        private NodeDrawer nodeDrawer;

        public SelectorNodeDrawer()
        {
            selectorSkin = Resources.Load("DoctorGUISkin") as GUISkin;
            nodeDrawer = new NodeDrawer();
        }

        public void Draw(List<SelectorNode> nodes)
        {
            if(nodes == null)
            {
                return;
            }

            foreach(var node in nodes)
            {
                var titleRect = node.rect;
                titleRect.height = 20f;
                titleRect.position += Vector2.down * 20f;
                GUI.Box(titleRect, node.selector.GetType().Name, selectorSkin.box);

                var qualifiersRect = new Rect(titleRect);
                qualifiersRect.width = node.rect.width * 0.5f;
                qualifiersRect.position = node.rect.position - node.rect.height * Vector2.down * 0.5f + node.rect.width * Vector2.right * 0.25f;

                DrawQualifierList(qualifiersRect,node);
            }

            nodeDrawer.Draw(nodes.Select(n => n as NodeBase).ToList());
        }

        private void DrawQualifierList(Rect qualifiersRect, SelectorNode node)
        {
            foreach (var qualifier in node.selector.qualifiers)
            {
                if (qualifier.name == null)
                {
                    qualifier.name = qualifier.GetType().Name;
                }

                if (GUI.Button(qualifiersRect, qualifier.name, selectorSkin.button))
                {
                    var qualifierEditor = EditorWindow.GetWindow<QualifierWindow>();
                    qualifierEditor.qualifier = qualifier;
                }

                qualifiersRect.position -= qualifiersRect.height * Vector2.down * 1.25f;
            }
        }
    }
}
