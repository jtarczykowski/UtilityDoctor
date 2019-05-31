using System;
using System.Collections.Generic;
using System.Linq;
using AmazingNodeEditor;
using UnityEditor;
using UnityEngine;

namespace UtilityDoctor.Editor
{
    public class UtilityDoctorEditor : NodeBasedEditor
    {
        protected List<SelectorNode> selectorNodes;
        protected List<ActionNode> actionNodes;

        [MenuItem("Window/UtilityDoctorEditor")]
        protected static void OpenUtilityDoctor()
        {
            var window = GetWindow<UtilityDoctorEditor>();
            window.titleContent = new GUIContent("Node Based Editor");
            window.Init();
        }

        UtilityDoctorRenderer windowRenderer;

        public void Init()
        {
            windowRenderer = new UtilityDoctorRenderer(this);
        }

        public List<Node> nodes;
        public List<Connection> connections;
        public List<ConnectionPin> connectionPins;

        protected void OnGUI()
        {
            windowRenderer.ProcessEvents(Event.current);
            windowRenderer.Render();
            if (GUI.changed)
            {
                Repaint();
            }
        }

        private SelectorNode CreateSelectorNode(Vector2 mousePosition,Selector selector)
        {
            var selectorNode = new SelectorNode();

            //var selectorNode =  new SelectorNode(selector,
            //    mousePosition,
            //    defaultNodeDimensions,
            //    defaultNodeStyle,
            //    OnClickInPoint,
            //    OnClickOutPoint,
            //    OnClickRemoveNode);
            return selectorNode;
        }

        private void OnClickAddSelector(Vector2 mousePosition, Selector selector)
        {
            if (nodes == null)
            {
                nodes = new List<Node>();
            }

            if(selectorNodes == null)
            {
                selectorNodes = new List<SelectorNode>();
            }

            var newNode = CreateSelectorNode(mousePosition, selector);
            nodes.Add(newNode);
            selectorNodes.Add(newNode);
        }

        public void CreateConnection(ConnectionPin selectedPin, ConnectionPin pin)
        {
            //TODO
        }
    }
}
