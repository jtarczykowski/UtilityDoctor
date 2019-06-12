using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

namespace UtilityDoctor.Editor
{
    public class InputConnectionPin : ConnectionPin
    {
        private const float connectionDistanceFromBox = 8f;
        [XmlIgnore]
        public NodeBase node;
        public string nodeId;

        public InputConnectionPin() { }

        public InputConnectionPin(NodeBase node, string id = null) : base(id)
        {
            this.node = node;
            nodeId = node.id;
        }

        public override void Update()
        {
            rect.y = node.rect.y + (node.rect.height * 0.5f) - (rect.height * 0.5f);
            rect.x = node.rect.x - rect.width + connectionDistanceFromBox;
        }
    }
}
