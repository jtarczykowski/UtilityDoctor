using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UtilityDoctor.ThirdParty;

namespace UtilityDoctor.Editor
{
    public class ConnectionPinFactory
    {
        private UtilityDoctorEditor window;

        public ConnectionPinFactory(UtilityDoctorEditor window)
        {
            this.window = window;
            Signals.Get<AddSelector>().AddListener(OnAddSelector);
        }

        private void OnAddSelector(Vector2 position, Selector selector)
        {
            var node = window
                .selectorNodes
                .Find(sn => sn.selector == selector);
            var inputPin = CreateInputConnectionPin(node, null);

            window.connectionPins.Add(inputPin);
        }

        public InputConnectionPin CreateInputConnectionPin(NodeBase node, string id = null)
        {
            return new InputConnectionPin(node,id);
        }

        public OutputConnectionPin CreateOutputConnectionPin(string id = null)
        {
            return new OutputConnectionPin(id);
        }
    }
}
