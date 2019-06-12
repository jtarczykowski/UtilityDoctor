using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using AmazingNodeEditor;
using UnityEditor;
using UnityEngine;
using UtilityDoctor.ThirdParty;

namespace UtilityDoctor.Editor
{
    public class UtilityDoctorEditor : EditorWindow
    {
        public List<SelectorNode> selectorNodes;
        public List<ActionNode> actionNodes;

        public ConnectionPinFactory pinFactory;

        [MenuItem("Window/UtilityDoctorEditor")]
        protected static void OpenUtilityDoctor()
        {
            var window = GetWindow<UtilityDoctorEditor>();
            window.titleContent = new GUIContent("Node Based Editor");
            window.Init();
        }

        UtilityDoctorRenderer windowRenderer;
        private MainWindowSerializer serializer;

        public void Init()
        {
            windowRenderer = new UtilityDoctorRenderer(this);
            serializer = new MainWindowSerializer(this);
            Signals.Get<AddSelector>().AddListener(OnAddSelector);
            connectionPins = new List<ConnectionPin>();
            pinFactory = new ConnectionPinFactory(this);
            connections = new List<Connection>();
            nodes = new List<NodeBase>();
            selectorNodes = new List<SelectorNode>();
            actionNodes = new List<ActionNode>();
        }

        public void Clear()
        {
            connectionPins?.Clear();
            connections?.Clear();
            nodes?.Clear();
            selectorNodes?.Clear();
            actionNodes?.Clear();
        }

        private void OnAddSelector(Vector2 position, Selector selector)
        {
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
            var selectorNode = new SelectorNode(selector,mousePosition,EditorConfig.GetDefaultNodeDimensions(), this);
            return selectorNode;
        }

        public void CreateConnection(InputConnectionPin inputPin, OutputConnectionPin outputPin)
        {
            var connection = new Connection(inputPin, outputPin);
            connections.Add(connection);
        }

        public void CreateActionNode(Vector2 mousePosition, Type type)
        {
            var action = Activator.CreateInstance(type) as ActionBase;
            var actionNode = new ActionNode(action, mousePosition, EditorConfig.GetDefaultNodeDimensions());
            nodes.Add(actionNode);
            actionNodes.Add(actionNode);
        }
    }
}
