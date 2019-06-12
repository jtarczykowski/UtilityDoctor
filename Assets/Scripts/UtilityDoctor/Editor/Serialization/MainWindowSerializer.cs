using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UtilityDoctor.ThirdParty;

namespace UtilityDoctor.Editor
{
    public class MainWindowSerializer 
    {
        private readonly UtilityDoctorEditor window;

        public MainWindowSerializer(UtilityDoctorEditor window)
        {
            this.window = window;
            Signals.Get<SaveButtonPressed>().AddListener(Serialize);
            Signals.Get<LoadButtonPressed>().AddListener(Deserialize);
        }

        protected string selectorNodesPath = $"{Application.streamingAssetsPath}/selectorNodes.xml";
        protected string actionNodesPath = $"{Application.streamingAssetsPath}/actionNodes.xml";
        protected string connectionsPath = $"{Application.streamingAssetsPath}/connections.xml";
        protected string pinsPath = $"{Application.streamingAssetsPath}/pins.xml";

        public void Serialize()
        {
            var selectorNodes = window.selectorNodes;
            XMLSaver.Serialize(selectorNodes, selectorNodesPath);

            var actionNodes = window.actionNodes;
            XMLSaver.Serialize(actionNodes, actionNodesPath);

            var connections = window.connections;
            XMLSaver.Serialize(connections, connectionsPath);

            var pins = window.connectionPins;
            XMLSaver.Serialize(pins, pinsPath);
        }

        public void Deserialize()
        {
            window.Clear();

            window.selectorNodes = XMLSaver.Deserialize<List<SelectorNode>>(selectorNodesPath);
            foreach(var node in window.selectorNodes)
            {
                node.Init();
                window.nodes.Add(node);
            }

            window.actionNodes = XMLSaver.Deserialize<List<ActionNode>>(actionNodesPath);
            foreach(var node in window.actionNodes)
            {
                window.nodes.Add(node);
            }

            window.connections = XMLSaver.Deserialize<List<Connection>>(connectionsPath);
            window.connectionPins = XMLSaver.Deserialize<List<ConnectionPin>>(pinsPath);

            var outputPins = new Dictionary<Qualifier, OutputConnectionPin>();

            foreach(var pin in window.connectionPins)
            {
                if(pin is InputConnectionPin inpin)
                {
                    var inputNode = window.nodes.Find(n => n.id == inpin.nodeId);
                    inpin.node = inputNode;
                }
                else if (pin is OutputConnectionPin outpin)
                {
                    foreach (var sn in window.selectorNodes)
                    {
                        foreach (var q in sn.selector.qualifiers)
                        {
                            if (q.id == outpin.ownerId)
                            {
                                outputPins.Add(q, outpin);
                            }
                        }
                    }
                }
            }

            foreach(var connection in window.connections)
            {
                connection.input = window.connectionPins.Find(p => p.id == connection.inputId) as InputConnectionPin;
                connection.output = window.connectionPins.Find(p => p.id == connection.outputId) as OutputConnectionPin;
            }

            Signals.Get<QualifierPinsLoaded>().Dispatch(outputPins);
        }
    }
}
