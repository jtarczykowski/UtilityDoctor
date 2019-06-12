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
    public partial class UtilityDoctorRenderer
    {
        protected SelectorNodeDrawer selectorNodeDrawer;
        protected ConnectionPinDrawer pinDrawer;
        protected ConnectionDrawer connectionDrawer;
        protected UtilityDoctorEditor window;

        protected ConnectionPin selectedPin;

        public UtilityDoctorRenderer(UtilityDoctorEditor window)
        {
            this.window = window;
            selectorNodeDrawer = new SelectorNodeDrawer(window);
            pinDrawer = new ConnectionPinDrawer();
            connectionDrawer = new ConnectionDrawer();

            Signals.Get<ConnectionPinClicked>().AddListener(OnClickPin);
        }

        protected Vector2 offset;
        protected Vector2 drag;

        public void Render()
        {
            GridDrawer.DrawGrid(EditorConfig.smallGridSpacing, EditorConfig.smallGridOpacity, window.position, ref offset, ref drag);
            GridDrawer.DrawGrid(EditorConfig.largeGridSpacing, EditorConfig.largeGridOpacity, window.position, ref offset, ref drag);
            MenuBarDrawer.DrawMenuBar(window);

            selectorNodeDrawer.Draw(window?.selectorNodes);
            pinDrawer.Draw(window.connectionPins);

            if (selectedPin != null)
            {
                connectionDrawer.DrawDraggedLine(selectedPin,Event.current.mousePosition);
            }
            connectionDrawer.DrawConnections(window.connections);
        }

        private NodeBase draggedNode;
        private NodeBase selectedNode;

        private void ProcessLeftClick(Vector2 mousePosition)
        {
            draggedNode = null;
            selectedNode = null;

            if(window?.nodes == null)
            {
                return;
            }

            foreach(var node in window.nodes)
            {
                if(node.rect.Contains(mousePosition))
                {
                    draggedNode = node;
                    selectedNode = node;
                    GUI.changed = true;
                }
            }
        }

        protected NodeBase FindNodeAtPosition(Vector2 position)
        {
            if(window.nodes == null)
            {
                return null;
            }

            foreach (var node in window.nodes)
            {
                if (node.rect.Contains(position))
                {
                    return node;
                }
            }

            return null;
        }

        protected void OnClickPin(ConnectionPin pin)
        {
            if(selectedPin == null)
            {
                selectedPin = pin;
                return;
            }

            if(selectedPin is InputConnectionPin inPin && pin is OutputConnectionPin outPin)
            {
                window.CreateConnection(inPin, outPin);
            }
            else if(selectedPin is OutputConnectionPin op && pin is InputConnectionPin ip)
            {
                window.CreateConnection(ip, op);
            }

            selectedPin = null;
        }

        private void OnClickCreateActionNode(Vector2 mousePosition, Type t)
        {
            //TODO
        }
    }
}
