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

        private void OnEnable()
        {
            qualifierWindowSkin = Resources.Load("QualifierWindowSkin") as GUISkin;
        }

        private void OnGUI()
        {
            qualifier.name = GUILayout.TextField(qualifier.name, qualifierWindowSkin.textField);
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

            for (int i = 0; i < scorers.Count; ++i)
            {
                var scorer = scorers[i];

                GUILayout.BeginHorizontal();
                GUILayout.Space(10f);

                scorer.ScorerName = GUILayout.TextField(scorer.ScorerName, qualifierWindowSkin.button);

                toggles[i] = GUILayout.Toggle(toggles[i],"select");
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

        private void AddNewScorer()
        {
            var window = GetWindow<AddScorerWindow>();
            window.Init(qualifier);
        }
    }
}
