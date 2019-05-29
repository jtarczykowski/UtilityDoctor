using AmazingNodeEditor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UtilityDoctor.Editor
{
    public class ConnectionPinDrawer
    {
        readonly GUIStyle pinStyle;

        public ConnectionPinDrawer(GUIStyle pinStyle = null)
        {
            this.pinStyle = this.pinStyle == null ? EditorConfig.CreateDefaultConnectionPinStyle() : pinStyle;
        }

        public void Draw(List<ConnectionPin> connectionPins)
        {
            foreach(var pin in connectionPins)
            {
                if(GUI.Button(pin.rect,string.Empty,pinStyle))
                {
                    Signals.Get<ConnectionPinClickedSignal>().Dispatch(pin);
                }
            }
        }
    }
}
