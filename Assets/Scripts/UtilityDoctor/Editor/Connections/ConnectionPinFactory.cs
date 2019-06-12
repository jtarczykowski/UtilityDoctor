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
            Signals.Get<AddAction>().AddListener(OnAddAction);
        }

        private void OnAddAction(Vector2 position, ActionBase action)
        {
            var node = window
                .actionNodes
                .Find(sn => sn.action == action);

            var inputPin = CreateInputConnectionPin(node, null);
            window.connectionPins.Add(inputPin);
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
