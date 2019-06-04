﻿using AmazingNodeEditor;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UtilityDoctor.ThirdParty;

namespace UtilityDoctor.Editor
{
    public class UtilityDoctorRenderer
    {
        protected SelectorNodeDrawer selectorNodeDrawer;
        protected ConnectionPinDrawer pinDrawer;
        protected ConnectionDrawer connectionDrawer;
        protected UtilityDoctorEditor window;

        protected ConnectionPin selectedPin;

        public UtilityDoctorRenderer(UtilityDoctorEditor window)
        {
            this.window = window;
            selectorNodeDrawer = new SelectorNodeDrawer();
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

        public void ProcessContextMenu(Vector2 mousePosition)
        {
            GenericMenu genericMenu = new GenericMenu();
            genericMenu.AddItem(new GUIContent("Add Selector/FirstScoreWins"), false,
                () => Signals.Get<AddSelector>()
                .Dispatch(mousePosition, new FirstScoreWinsSelector())
                );
            genericMenu.AddItem(new GUIContent("Add Selector/HighestScoreWins"), false,
                () => Signals.Get<AddSelector>()
                .Dispatch(mousePosition, new HighestScoreWinsSelector())
                );


            var actionTypes = typeof(ActionBase).Assembly
                .GetTypes()
                .Where(t => t.IsSubclassOf(typeof(ActionBase)))
                .ToArray();

            foreach (var t in actionTypes)
            {
                genericMenu.AddItem(new GUIContent($"Add Action/{t.Name}"), false,
                    () => OnClickCreateActionNode(mousePosition, t));
            }

            genericMenu.ShowAsContext();
        }

        public void ProcessEvents(Event e)
        {
            drag = Vector2.zero;

            switch (e.type)
            {
                case EventType.MouseDown:
                    if (e.button == 0)
                    {
                        ProcessLeftClick(e.mousePosition);
                    }

                    if (e.button == 1)
                    {
                        ProcessContextMenu(e.mousePosition);
                        e.Use();
                    }
                    break;

                case EventType.MouseDrag:
                    if (e.button == 0)
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
                        e.Use();
                    }
                    break;

                case EventType.MouseUp:
                    StopDragging();
                    break;
            }
        }

        private void StopDragging()
        {
            draggedNode = null;
        }

        private NodeBase draggedNode;
        private NodeBase selectedNode;

        private void ProcessLeftClick(Vector2 mousePosition)
        {
            draggedNode = null;
            selectedNode = null;

            foreach(var node in window.nodes)
            {
                if(node.rect.Contains(mousePosition))
                {
                    draggedNode = node;
                    selectedNode = node;
                    GUI.changed = true;
                }
            }

            ClearConnectionSelection();
        }

        protected NodeBase FindNodeAtPosition(Vector2 position)
        {
            foreach (var node in window.nodes)
            {
                if (node.rect.Contains(position))
                {
                    return node;
                }
            }

            return null;
        }

        protected void ClearConnectionSelection()
        {
            selectedPin = null;
        }

        protected void OnDragAll(Vector2 delta)
        {
            drag = delta;

            if (window.nodes != null)
            {
                foreach(var node in window.nodes)
                {
                    DragNode(delta, node);
                }
            }

            GUI.changed = true;
        }

        protected void DragNode(Vector2 delta,NodeBase node)
        {
            node.rect.position += delta;
        }

        protected void OnClickPin(ConnectionPin pin)
        {
            if(selectedPin == null)
            {
                selectedPin = pin;
                return;
            }

            if(selectedPin is InputConnectionPin && pin is OutputConnectionPin)
            {
                window.CreateConnection(selectedPin, pin);
            }
            else if(selectedPin is OutputConnectionPin && pin is OutputConnectionPin)
            {
                window.CreateConnection(pin, selectedPin);
            }

            ClearConnectionSelection();
        }

        private void OnClickCreateActionNode(Vector2 mousePosition, Type t)
        {
            //TODO
        }
    }
}
