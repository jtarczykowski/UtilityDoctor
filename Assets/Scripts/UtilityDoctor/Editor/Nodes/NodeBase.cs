using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UtilityDoctor.Editor
{
    public class NodeBase
    {
        public string id;
        public Rect rect;
        public string title;

        public NodeBase() { }
        public NodeBase(Vector2 position, Vector2 dimensions, string id = null)
        {
            rect = new Rect(position.x, position.y, dimensions.x, dimensions.y);
            this.id = id ?? System.Guid.NewGuid().ToString();
        }
    }
}
