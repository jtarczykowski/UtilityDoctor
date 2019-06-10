using AmazingNodeEditor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UtilityDoctor.ThirdParty;

namespace UtilityDoctor.Editor
{
    public class ConnectionPinDrawer
    {
        readonly GUIStyle inputPinStyle;
        readonly GUIStyle outputPinStyle;

        public ConnectionPinDrawer()
        {
            inputPinStyle = EditorConfig.CreateInputConnectionPinStyle();
            outputPinStyle = EditorConfig.CreateOutputConnectionPinStyle();
        }

        public void Draw(List<ConnectionPin> connectionPins)
        {
            if(connectionPins == null)
            {
                return;
            }

            foreach(var pin in connectionPins)
            {
                pin.Update();
                var style = (pin is InputConnectionPin) ? inputPinStyle : outputPinStyle;

                if (GUI.Button(pin.rect,string.Empty,style))
                {
                    Signals.Get<ConnectionPinClicked>().Dispatch(pin);
                }
            }
        }
    }
}
