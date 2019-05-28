﻿using System;
using System.Linq;
using AmazingNodeEditor;
using UnityEditor;
using UnityEngine;

namespace UtilityDoctor.Editor
{
    public class SelectorNode : Node
    {
        public Selector selector;
        public string nodeName = "selector";
        GUISkin selectorSkin;
        QualifierListVisualizer qualifierListVisualizer;

        public override void Draw()
        {
            base.Draw();

            if(selector != null)
            {
                var titleRect = rect;
                titleRect.height = 20f;
                titleRect.position += Vector2.down * 20f;
                
                GUI.Box(titleRect, selector.GetType().Name,selectorSkin.box);
                var qualifiersRect = new Rect(titleRect);
                qualifiersRect.width = rect.width * 0.5f;
                qualifiersRect.position = rect.position - rect.height * Vector2.down * 0.5f + rect.width * Vector2.right * 0.25f;

                qualifierListVisualizer.Draw(qualifiersRect);
            }
        }

        protected override void ProcessContextMenu()
        {
            GenericMenu genericMenu = new GenericMenu();
            genericMenu.AddItem(new GUIContent(removeNodeText), false, OnClickRemoveNode);

            var qualifierTypes = typeof(Qualifier).Assembly
                .GetTypes()
                .Where(t => t.IsSubclassOf(typeof(Qualifier)))
                .ToArray();

            foreach(var t in qualifierTypes)
            {
                genericMenu.AddItem(new GUIContent($"Add Qualifier/{t.Name}"), false,
                    () => OnClickCreateQualifier(t));
            }

            genericMenu.ShowAsContext();
        }

        private void OnClickCreateQualifier(Type t)
        {
            var qualifier = Activator.CreateInstance(t);
            selector.qualifiers.Add(qualifier as Qualifier);
            rect.height += selectorSkin.button.fixedHeight;
        }

        public SelectorNode(Selector selector, Vector2 position, Vector2 dimensions, NodeStyleInfo styleInfo,
            Action<ConnectionPointBase> onClickInPoint, Action<ConnectionPointBase> onClickOutPoint,
            Action<Node> onClickRemoveNode, string inPointId = null, string outPointId = null) :
            base(position, dimensions, styleInfo, onClickInPoint, onClickOutPoint, onClickRemoveNode, inPointId, outPointId)
        {
            this.selector = selector;
            selectorSkin = Resources.Load("DoctorGUISkin") as GUISkin;
            qualifierListVisualizer = new QualifierListVisualizer(selector, selectorSkin);
        }

        public SelectorNode() { }
    }
}
