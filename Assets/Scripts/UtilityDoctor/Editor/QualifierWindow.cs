using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace UtilityDoctor.Editor
{
    public class QualifierWindow : EditorWindow
    {
        public Qualifier qualifier;

        private GUISkin qualifierWindowSkin;

        private List<bool> toggles = new List<bool>();
        private List<Type> scorerTypes;

        private void OnEnable()
        {
            qualifierWindowSkin = Resources.Load("QualifierWindowSkin") as GUISkin;
        }

        private void OnGUI()
        {
            GUILayout.Label("Qualifier name",qualifierWindowSkin.label);
            GUILayout.Space(2f);

            qualifier.name = GUILayout.TextField(qualifier.name, qualifierWindowSkin.textField);

            GUILayout.Label("Qualifier description", qualifierWindowSkin.label);
            GUILayout.Space(2f);

            qualifier.description = GUILayout.TextArea(qualifier.description, qualifierWindowSkin.textArea);

            var scorers = qualifier.scorers;


            if (toggles.Count != scorers.Count)
            {
                toggles.Clear();
                foreach (var scorer in scorers)
                {
                    toggles.Add(false);
                }
            }

            GUILayout.Space(10f);
            GUILayout.Label("Scorers:", qualifierWindowSkin.label);
            GUILayout.Space(2f);


            for (int i = 0; i < scorers.Count; ++i)
            {
                var scorer = scorers[i];

                GUILayout.BeginHorizontal();
                GUILayout.Space(10f);

                toggles[i] = GUILayout.Toggle(toggles[i],string.Empty);
                GUILayout.Space(5f);
                
                if(EditorGUILayout.DropdownButton( new GUIContent(scorer.GetType().Name),FocusType.Keyboard))
                {
                    var rect = GUILayoutUtility.GetLastRect();
                    var dropdownMenu = new GenericMenu();

                    scorerTypes = typeof(Scorer).Assembly
                    .GetTypes()
                    .Where(t => t.IsSubclassOf(typeof(Scorer)))
                    .ToList();

                    foreach(var sc in scorerTypes)
                    {
                        string name = sc.Name;
                        dropdownMenu.AddItem(new GUIContent(name),false,() => ChangeScorerType(i,sc));
                    }

                    dropdownMenu.ShowAsContext();
                    return;
                }

                scorer.ScorerName = GUILayout.TextField(scorer.ScorerName, qualifierWindowSkin.button);

                GUILayout.EndHorizontal();

                GUILayout.Space(2f);
            }

            GUILayout.Space(10f);

            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Add new scorer", qualifierWindowSkin.button))
            {
                AddNewScorer();
            }
            if (GUILayout.Button("Remove selected", qualifierWindowSkin.button))
            {
                for (int i = toggles.Count - 1; i >= 0; --i)
                {
                    if(toggles[i])
                    {
                        qualifier.scorers.RemoveAt(i);
                    }
                }
            }
            GUILayout.EndHorizontal();
        }

        private void ChangeScorerType(int i, Type type)
        {
            var scorer = Activator.CreateInstance(type) as Scorer;
            scorer.ScorerName = scorer.GetType().Name;
            qualifier.scorers[i] = scorer;
            GUI.changed = true;
        }

        private void AddNewScorer()
        {
            var window = GetWindow<AddScorerWindow>();
            window.position = this.position;
            window.Init(qualifier);
        }
    }
}
