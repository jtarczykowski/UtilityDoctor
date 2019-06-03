using AmazingNodeEditor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UtilityDoctor.ThirdParty;

namespace UtilityDoctor.Editor
{
    public class ConnectionPinDrawer
    {
        readonly GUIStyle pinStyle;

        public ConnectionPinDrawer()
        {
            pinStyle = EditorConfig.CreateDefaultConnectionPinStyle();
        }

        public void Draw(List<ConnectionPin> connectionPins)
        {
            if(connectionPins == null)
            {
                return;
            }

            foreach(var pin in connectionPins)
            {
                if(GUI.Button(pin.rect,string.Empty,pinStyle))
                {
                    Signals.Get<ConnectionPinClicked>().Dispatch(pin);
                }
            }
        }
    }
}
