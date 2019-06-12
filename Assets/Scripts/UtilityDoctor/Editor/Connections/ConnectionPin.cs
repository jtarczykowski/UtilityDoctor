using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

namespace UtilityDoctor.Editor
{
    [XmlInclude(typeof(InputConnectionPin))]
    [XmlInclude(typeof(OutputConnectionPin))]
    public class ConnectionPin
    {
        public string id;
        public Rect rect;

        public ConnectionPin() { }

        public ConnectionPin(string id = null)
        {
            this.id = id ?? System.Guid.NewGuid().ToString();
            rect = new Rect(Vector2.zero, new Vector2(10f, 20f));
        }

        public virtual void Update() { }
    }
}
