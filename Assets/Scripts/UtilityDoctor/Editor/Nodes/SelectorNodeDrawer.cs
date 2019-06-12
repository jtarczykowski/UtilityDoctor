using AmazingNodeEditor;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UtilityDoctor.ThirdParty;

namespace UtilityDoctor.Editor
{
    public class SelectorNodeDrawer
    {
        readonly GUISkin selectorSkin;
        private NodeDrawer nodeDrawer;
        private UtilityDoctorEditor window;

        public SelectorNodeDrawer(UtilityDoctorEditor window)
        {
            selectorSkin = Resources.Load("DoctorGUISkin") as GUISkin;
            nodeDrawer = new NodeDrawer();
            this.window = window;
            Signals.Get<QualifierPinsLoaded>().AddListener(OnQualifierPinsLoaded);
        }

        private void OnQualifierPinsLoaded(Dictionary<Qualifier, OutputConnectionPin> qp)
        {
            outputPins = qp;
        }

        public void Draw(List<SelectorNode> nodes)
        {
            if(nodes == null)
            {
                return;
            }

            nodeDrawer.Draw(nodes.Select(n => n as NodeBase).ToList());

            foreach (var node in nodes)
            {
                var titleRect = node.rect;
                titleRect.height = 20f;
                titleRect.position += Vector2.down * 20f;
                GUI.Box(titleRect, node.selector.GetType().Name, selectorSkin.box);

                var qualifiersRect = new Rect(titleRect);
                qualifiersRect.width = node.rect.width * 0.5f;
                qualifiersRect.position = node.rect.position - node.rect.height * Vector2.down * 0.5f + node.rect.width * Vector2.right * 0.25f;

                DrawQualifierList(ref qualifiersRect,node);
            }
        }

        private Dictionary<Qualifier, OutputConnectionPin> outputPins = new Dictionary<Qualifier, OutputConnectionPin>();

        private OutputConnectionPin GetQualifierConnectionPin(Qualifier qualifier)
        {
            if (!outputPins.ContainsKey(qualifier))
            {
                var pin = window.pinFactory.CreateOutputConnectionPin();
                outputPins.Add(qualifier, pin);
                pin.ownerId = qualifier.id;
                window.connectionPins.Add(pin);
            }

            return outputPins[qualifier];
        }

        private void DrawQualifierList(ref Rect qualifiersRect, SelectorNode node)
        {
            foreach (var qualifier in node.selector.qualifiers)
            {
                if (qualifier.name == null)
                {
                    qualifier.name = qualifier.GetType().Name;
                }
                GUILayout.BeginHorizontal();
                if (GUI.Button(qualifiersRect, qualifier.name, selectorSkin.button))
                {
                    var qualifierEditor = EditorWindow.GetWindow<QualifierWindow>();
                    qualifierEditor.qualifier = qualifier;
                }

                var pin = GetQualifierConnectionPin(qualifier);
                var offset = new Vector2(qualifiersRect.width, 0);
                pin.rect.position = qualifiersRect.position + offset;
                GUILayout.EndHorizontal();
                qualifiersRect.position -= qualifiersRect.height * Vector2.down * 1.25f;
            }
        }

        public void ProcessContextMenu(SelectorNode node)
        {
            GenericMenu genericMenu = new GenericMenu();
            //genericMenu.AddItem(new GUIContent(removeNodeText), false, 
            //    );

            var qualifierTypes = typeof(Qualifier).Assembly
                .GetTypes()
                .Where(t => t.IsSubclassOf(typeof(Qualifier)))
                .ToArray();

            foreach (var t in qualifierTypes)
            {
                genericMenu.AddItem(new GUIContent($"Add Qualifier/{t.Name}"), false,
                    () => Signals.Get<AddQualifier>().Dispatch(node,t));
            }

            genericMenu.ShowAsContext();
        }
    }
}
