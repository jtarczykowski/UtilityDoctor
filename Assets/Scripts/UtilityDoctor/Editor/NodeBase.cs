using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UtilityDoctor.Editor
{
    public class NodeBase
    {
        public Rect rect;
        public string title;

        public NodeBase() { }
        public NodeBase(Vector2 position, Vector2 dimensions)
        {
            rect = new Rect(position.x, position.y, dimensions.x, dimensions.y);
        }
    }
}
