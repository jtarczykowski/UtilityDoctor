using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace UtilityDoctor.Editor
{
    public class QualifierListVisualizer 
    {
        private Selector selector;
        private GUISkin skin;

        public QualifierListVisualizer(Selector selector, GUISkin skin)
        {
            this.selector = selector;
            this.skin = skin;
        }

        public void Draw(Rect qualifiersRect)
        {
            foreach (var qualifier in selector.qualifiers)
            {
                if (qualifier.name == null)
                {
                    qualifier.name = qualifier.GetType().Name;
                }

                if (GUI.Button(qualifiersRect, qualifier.name, skin.button))
                {
                    var qualifierEditor = EditorWindow.GetWindow<QualifierWindow>();
                    qualifierEditor.qualifier = qualifier;
                }

                qualifiersRect.position -= qualifiersRect.height * Vector2.down * 1.25f;
            }
        }
    }
}
