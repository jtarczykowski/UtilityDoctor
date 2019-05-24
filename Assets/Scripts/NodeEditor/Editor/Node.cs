using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEditor;
using UnityEngine;

namespace AmazingNodeEditor
{
    public class Node
    {
        public Rect rect;

        [XmlIgnore]
        public string title;
        [XmlIgnore]
        public bool isDragged;
        [XmlIgnore]
        public bool isSelected;

        public NodeConnectionPoint inPoint;
        public NodeConnectionPoint outPoint;

        [XmlIgnore]
        public NodeStyleInfo style;

        [XmlIgnore]
        public Action<Node> OnRemoveNode;

        public Node() { }

        public Node(Vector2 position, Vector2 dimensions, NodeStyleInfo styleInfo, 
            Action<ConnectionPointBase> onClickInPoint, Action<ConnectionPointBase> onClickOutPoint,
            Action<Node> onClickRemoveNode, string inPointId = null, string outPointId = null)
        {
            rect = new Rect(position.x, position.y, dimensions.x, dimensions.y);
            inPoint = new NodeConnectionPoint(this, ConnectionPointType.In, styleInfo.inPointStyle, onClickInPoint,inPointId);
            outPoint = new NodeConnectionPoint(this, ConnectionPointType.Out, styleInfo.outPointStyle, onClickOutPoint, outPointId);
            OnRemoveNode = onClickRemoveNode;
            style = styleInfo;
        }

        public void Drag(Vector2 delta)
        {
            rect.position += delta;
        }

        public virtual void Draw()
        {
            inPoint.Draw();
            outPoint.Draw();
            var currentStyle = isSelected ? style.selectedNodeStyle : style.defaultNodeStyle;
            GUI.Box(rect, title, currentStyle);
        }

        public bool ProcessEvents(Event e)
        {
            switch (e.type)
            {
                case EventType.MouseDown:
                    if (e.button == 0)
                    {
                        if (rect.Contains(e.mousePosition))
                        {
                            isDragged = true;
                            GUI.changed = true;
                            isSelected = true;
                        }
                        else
                        {
                            GUI.changed = true;
                            isSelected = false;
                        }
                    }

                    if(e.button == 1 && rect.Contains(e.mousePosition))
                    {
                        ProcessContextMenu();
                        e.Use();
                    }

                    break;

                case EventType.MouseUp:
                    isDragged = false;
                    break;

                case EventType.MouseDrag:
                    if (e.button == 0 && isDragged)
                    {
                        Drag(e.delta);
                        e.Use();
                        return true;
                    }
                    break;
            }

            return false;
        }

        protected const string removeNodeText = "Remove node";
        protected virtual void ProcessContextMenu()
        {
            GenericMenu genericMenu = new GenericMenu();
            genericMenu.AddItem(new GUIContent(removeNodeText), false, OnClickRemoveNode);
            genericMenu.ShowAsContext();
        }

        protected void OnClickRemoveNode()
        {
            OnRemoveNode?.Invoke(this);
        }
    }

    public struct NodeStyleInfo
    {
        public GUIStyle currentStyle;
        public GUIStyle defaultNodeStyle;
        public GUIStyle selectedNodeStyle;
        public GUIStyle inPointStyle;
        public GUIStyle outPointStyle;
    }
}
