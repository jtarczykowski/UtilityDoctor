using AmazingNodeEditor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UtilityDoctor.Editor
{
    public class QualifierOutConnectionPoint : NodeConnectionPoint
    {
        public Qualifier qualifier;
        //this has to be set by whatever is drawing the connection points
        public Rect OutPointRect;

        public override Rect GetButtonRect
        {
            get
            {
                return OutPointRect;
            }
        }
    }
}
