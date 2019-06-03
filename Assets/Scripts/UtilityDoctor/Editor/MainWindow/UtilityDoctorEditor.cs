﻿using System;
using System.Collections.Generic;
using System.Linq;
using AmazingNodeEditor;
using UnityEditor;
using UnityEngine;
using UtilityDoctor.ThirdParty;

namespace UtilityDoctor.Editor
{
    public class UtilityDoctorEditor : EditorWindow
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
            Signals.Get<AddSelector>().AddListener(OnAddSelector);
        }

        private void OnAddSelector(Vector2 position, Selector selector)
        {
            if (nodes == null)
            {
                nodes = new List<NodeBase>();
            }

            if (selectorNodes == null)
            {
                selectorNodes = new List<SelectorNode>();
            }

            var newNode = CreateSelectorNode(position, selector);
            nodes.Add(newNode);
            selectorNodes.Add(newNode);
        }

        public List<NodeBase> nodes;
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
            var selectorNode = new SelectorNode(selector,mousePosition,EditorConfig.GetDefaultNodeDimensions());
            return selectorNode;
        }

        public void CreateConnection(ConnectionPin selectedPin, ConnectionPin pin)
        {
            //TODO
        }
    }
}
