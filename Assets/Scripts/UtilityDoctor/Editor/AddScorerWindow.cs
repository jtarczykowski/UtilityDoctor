using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace UtilityDoctor.Editor
{
    public class AddScorerWindow : EditorWindow
    {
        private List<Type> scorerTypes = new List<Type>();
        private Qualifier qualifier;

        public void Init(Qualifier qualifier)
        {
            this.qualifier = qualifier;

            scorerTypes = typeof(Scorer).Assembly
                .GetTypes()
                .Where(t => t.IsSubclassOf(typeof(Scorer)))
                .ToList();
        }

        private void OnGUI()
        {
            foreach (var type in scorerTypes)
            {
                if (GUILayout.Button(type.Name))
                {
                    var scorer = Activator.CreateInstance(type) as Scorer;
                    qualifier.scorers.Add(scorer);
                    GUI.changed = true;
                    Close();
                    return;
                }
            }
        }
    }
}


