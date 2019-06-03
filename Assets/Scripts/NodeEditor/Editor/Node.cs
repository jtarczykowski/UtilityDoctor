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
