using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEditor;
using UnityEngine;

namespace AmazingNodeEditor
{
    public class Connection
    {
        public NodeConnectionPoint inPoint;
        public NodeConnectionPoint outPoint;

        [XmlIgnore]
        public Action<Connection> OnClickRemoveConnection;

        public Connection() { }

        public Connection(NodeConnectionPoint inPoint, NodeConnectionPoint outPoint, Action<Connection> onClickRemoveConnection)
        {
            this.inPoint = inPoint;
            this.outPoint = outPoint;
            this.OnClickRemoveConnection = onClickRemoveConnection;
        }

        private static Vector2 bezierOffset = Vector2.left * 50f;
        private const float defaultHandleWidth = 2f;

        public void Draw()
        {
            var inCenter = inPoint.rect.center;
            var outCenter = outPoint.rect.center;

            Handles.DrawBezier(inCenter, outCenter,
                inCenter + bezierOffset, outCenter - bezierOffset,
                Color.white, null, defaultHandleWidth);

            bool removeClicked = Handles.Button((inCenter + outCenter) * 0.5f,
                Quaternion.identity, 4, 8, Handles.RectangleHandleCap);

            if (removeClicked)
            {
                OnClickRemoveConnection?.Invoke(this);
            }
        }
    }
}
