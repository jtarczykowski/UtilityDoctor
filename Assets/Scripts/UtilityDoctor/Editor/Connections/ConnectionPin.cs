using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UtilityDoctor.Editor
{
    public class ConnectionPin
    {
        public string id;
        public Rect rect;

        public ConnectionPin(string id = null)
        {
            this.id = id ?? System.Guid.NewGuid().ToString();
        }

        public virtual void Update() { }
    }
}
