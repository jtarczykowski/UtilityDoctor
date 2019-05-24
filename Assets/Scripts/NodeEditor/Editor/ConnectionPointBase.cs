using System;
using System.Xml.Serialization;
using UnityEngine;

namespace AmazingNodeEditor
{
    public enum ConnectionPointType { In, Out }

    public class ConnectionPointBase
    {
        public string id;

        [XmlIgnore]
        public Rect rect;
        [XmlIgnore]
        public ConnectionPointType type;

        [XmlIgnore]
        public Action<ConnectionPointBase> OnClickConnectionPoint;

        [XmlIgnore]
        public GUIStyle style;

        public virtual Rect GetButtonRect { get; }

        public ConnectionPointBase() { }

        public ConnectionPointBase(ConnectionPointType type, GUIStyle style, 
            Action<ConnectionPointBase> onClickConnectionPoint, string id = null)
        {
            this.type = type;
            this.style = style;
            this.OnClickConnectionPoint = onClickConnectionPoint;
            rect = new Rect(0, 0, 10f, 20f);

            this.id = id ?? Guid.NewGuid().ToString();
        }

        public void Draw()
        {
            rect = GetButtonRect;

            if (GUI.Button(rect, "", style))
            {
                OnClickConnectionPoint?.Invoke(this);
            }
        }
    }
}
