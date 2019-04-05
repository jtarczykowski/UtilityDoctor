using AmazingNodeEditor;
using UnityEngine;

namespace UtilityDoctor.Editor
{
    public class SelectorNode : Node
    {
        public Selector selector;
        public string nodeName = "selector";

        public override void Draw()
        {
            base.Draw();
            if(selector != null)
            {
                foreach (var qualifier in selector.qualifiers)
                {
                    var typeName = qualifier.GetType().Name;
                    GUILayout.Label(typeName);
                }
            }
        }
    }
}
