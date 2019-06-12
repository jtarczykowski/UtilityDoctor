using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UtilityDoctor.Editor
{
    public class OutputConnectionPin : ConnectionPin
    {
        public string ownerId;

        public OutputConnectionPin() { }

        public OutputConnectionPin(string id = null) : base(id)
        {

        }
    }
}
