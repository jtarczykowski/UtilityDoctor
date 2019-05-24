using System;
using System.Xml.Serialization;
using UnityEngine;

namespace AmazingNodeEditor
{
    public class NodeConnectionPoint : ConnectionPointBase
    {
        [XmlIgnore]
        public Node node;

        public NodeConnectionPoint() { }

        public NodeConnectionPoint(Node node, ConnectionPointType type,GUIStyle style, 
            Action<ConnectionPointBase> onClickConnectionPoint, string id = null)
            : base(type,style,onClickConnectionPoint)
        {
            this.node = node;
        }

        private float connectionDistanceFromBox = 8f;

        public override Rect GetButtonRect
        {
            get
            {
                var baseRect = rect;

                baseRect.y = node.rect.y + (node.rect.height * 0.5f) - (rect.height * 0.5f);

                switch (type)
                {
                    case ConnectionPointType.In:
                        baseRect.x = node.rect.x - baseRect.width + connectionDistanceFromBox;
                        break;

                    case ConnectionPointType.Out:
                        baseRect.x = node.rect.x + node.rect.width - connectionDistanceFromBox;
                        break;
                }

                return baseRect;
            }
        }
    }
}
