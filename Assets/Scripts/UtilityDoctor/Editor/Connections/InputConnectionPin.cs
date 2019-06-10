using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UtilityDoctor.Editor
{
    public class InputConnectionPin : ConnectionPin
    {
        private const float connectionDistanceFromBox = 8f;
        public NodeBase node;

        public InputConnectionPin(NodeBase node, string id = null) : base(id)
        {
            this.node = node;
            rect = new Rect(Vector2.zero, new Vector2(10f, 20f));
        }

        public override void Update()
        {
            rect.y = node.rect.y + (node.rect.height * 0.5f) - (rect.height * 0.5f);
            rect.x = node.rect.x - rect.width + connectionDistanceFromBox;
        }
    }
}
