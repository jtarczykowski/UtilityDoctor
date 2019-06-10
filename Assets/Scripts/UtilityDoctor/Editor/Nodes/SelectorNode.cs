using System;
using System.Linq;
using System.Xml.Serialization;
using AmazingNodeEditor;
using UnityEditor;
using UnityEngine;
using UtilityDoctor.ThirdParty;

namespace UtilityDoctor.Editor
{
    public class SelectorNode : NodeBase
    {
        public Selector selector;
        public string nodeName = "selector";
        GUISkin selectorSkin;

        public SelectorNode(Selector selector, Vector2 position, Vector2 dimensions, UtilityDoctorEditor window) 
            : base(position, dimensions)
        {
            this.selector = selector;
            Init();
        }

        public void Init()
        {
            selectorSkin = Resources.Load("DoctorGUISkin") as GUISkin;
            Signals.Get<AddQualifier>().AddListener(OnAddQualifier);
        }

        private void OnAddQualifier(SelectorNode node, Type type)
        {
            if(node == this)
            {
                var qualifier = Activator.CreateInstance(type);
                selector.qualifiers.Add(qualifier as Qualifier);
                rect.height += selectorSkin.button.fixedHeight;
            }
        }

        public SelectorNode() { }
    }
}
