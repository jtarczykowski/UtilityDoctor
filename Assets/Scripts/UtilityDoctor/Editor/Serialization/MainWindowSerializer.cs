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

        public void Serialize()
        {
            var selectorNodes = window.selectorNodes;
            if(selectorNodes != null)
            {
                XMLSaver.Serialize(selectorNodes, selectorNodesPath);
            }
        }

        public void Deserialize()
        {
            window.Clear();

            var nodesDeserialized = XMLSaver.Deserialize<List<SelectorNode>>(selectorNodesPath);
            window.selectorNodes = nodesDeserialized;
            foreach(var node in nodesDeserialized)
            {
                node.Init();
                window.nodes.Add(node);
            }
        }
    }
}
