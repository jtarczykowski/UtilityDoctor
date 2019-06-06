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
        QualifierListVisualizer qualifierListVisualizer;

        public void Draw()
        {
            if(selector != null)
            {
                var titleRect = rect;
                titleRect.height = 20f;
                titleRect.position += Vector2.down * 20f;
                
                GUI.Box(titleRect, selector.GetType().Name,selectorSkin.box);
                var qualifiersRect = new Rect(titleRect);
                qualifiersRect.width = rect.width * 0.5f;
                qualifiersRect.position = rect.position - rect.height * Vector2.down * 0.5f + rect.width * Vector2.right * 0.25f;

                qualifierListVisualizer.Draw(qualifiersRect);
            }
        }

        public SelectorNode(Selector selector, Vector2 position, Vector2 dimensions) 
            : base(position, dimensions)
        {
            this.selector = selector;
            qualifierListVisualizer = new QualifierListVisualizer(selector, selectorSkin);
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
